using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Api.Controllers;
using Order.Service.Services.OrderService.Dto;
using Order.Service.Services.PaymentService;
using Stripe;

namespace Order.API.Controllers
{
    //[Authorize(Roles = "Admin")]

    public class PaymentController : BaseController
    {
        private readonly IPaymentService _paymentService;
        private readonly ILogger<PaymentController> _logger;
        private const string endpointSecret = "whsec_33ccdc287179ca6ef260e0c7bae55c0d2f43e03b2630b612b38796842f2951c3";

        public PaymentController(
            IPaymentService paymentService,
            ILogger<PaymentController> logger
            )
        {
            _paymentService = paymentService;
            _logger = logger;
        }

        [HttpPost("orderId")]
        public async Task<ActionResult<OrderBasketDto>> CreateOrUpdatePaymentIntentForExistingOrder(OrderBasketDto input)
         => Ok(await _paymentService.CreateOrUpdatePaymentIntentForExistingOrder(input));

        [HttpPost("orderId")]
        public async Task<ActionResult<OrderBasketDto>> CreateOrUpdatePaymentIntentForNewOrder(Guid orderId)
        => Ok(await _paymentService.CreateOrUpdatePaymentIntentForNewOrder(orderId));

        [AllowAnonymous]
        [HttpPost("webhook")]
        public async Task<IActionResult> Index()
        {
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            try
            {
                var stripeEvent = EventUtility.ConstructEvent(json,
                    Request.Headers["Stripe-Signature"], endpointSecret);

                PaymentIntent paymentIntent;
                OrderResultDto order;
                // Handle the event
                if (stripeEvent.Type == Events.PaymentIntentPaymentFailed)
                {
                    paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Failed : ", paymentIntent.Id);
                    order = await _paymentService.UpdateOrderPaymentFailed(paymentIntent.Id);
                    _logger.LogInformation("Order Updated To Payment Failed : ", order.OrdersId);
                }
                else if (stripeEvent.Type == Events.PaymentIntentSucceeded)
                {
                    paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
                    _logger.LogInformation("Payment Succeeded : ", paymentIntent.Id);
                    order = await _paymentService.UpdateOrderPaymentSucceeded(paymentIntent.Id);
                    _logger.LogInformation("Order Updated To Payment Succeeded : ", order.OrdersId);
                }
                // ... handle other event types
                else
                {
                    Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
                }

                return Ok();
            }
            catch (StripeException e)
            {
                return BadRequest();
            }
        }

    }
}

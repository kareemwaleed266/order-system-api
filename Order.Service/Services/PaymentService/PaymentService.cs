using AutoMapper;
using Microsoft.Extensions.Configuration;
using Order.Data.Entities.OrderEntities;
using Order.Repository.Interfaces;
using Order.Service.Services.OrderService.Dto;
using Stripe;


namespace Order.Service.Services.PaymentService
{
    public class PaymentService : IPaymentService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PaymentService(IConfiguration configuration,
            IUnitOfWork unitOfWork,
            IMapper mapper
            )
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<OrderBasketDto> CreateOrUpdatePaymentIntentForExistingOrder(OrderBasketDto input)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:Secretkey"];

            if (input == null)
                throw new Exception("Order Not Found");

            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;

            if (string.IsNullOrEmpty(input.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)input.TotalAmount,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                paymentIntent = await service.CreateAsync(options);

                input.PaymentIntentId = paymentIntent.Id;
                input.ClientSercret = paymentIntent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)input.TotalAmount
                };

                await service.UpdateAsync(input.PaymentIntentId, options);
            }
            return input;
        }
        public async Task<OrderBasketDto> CreateOrUpdatePaymentIntentForNewOrder(Guid orderId)
        {
            StripeConfiguration.ApiKey = _configuration["Stripe:Secretkey"];
            if (orderId == null)
                throw new Exception("Order Not Found");

            var order = await _unitOfWork.Repository<Orders, Guid>().GetByIdAsync(orderId);
            var service = new PaymentIntentService();
            PaymentIntent paymentIntent;

            if (string.IsNullOrEmpty(order.PaymentIntentId))
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = (long)order.TotalAmount,
                    Currency = "usd",
                    PaymentMethodTypes = new List<string> { "card", "PayPal" }
                };

                paymentIntent = await service.CreateAsync(options);

                order.PaymentIntentId = paymentIntent.Id;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = (long)order.TotalAmount
                };

                await service.UpdateAsync(order.PaymentIntentId, options);
            }

            return _mapper.Map<OrderBasketDto>(order);
        }

        public async Task<OrderResultDto> UpdateOrderPaymentFailed(string paymentIntentId)
        {
            var orderExist = await _unitOfWork.Repository<Orders, Guid>().GetIfExistsAsync(x => x.PaymentIntentId == paymentIntentId);
            var order = await _unitOfWork.Repository<Orders, Guid>().GetByIdAsync(orderExist.Id);

            if (order is null)
                throw new Exception("Order Does Not Exist");

            order.OrderPayementStatus = OrderPayementStatus.Failed;

            _unitOfWork.Repository<Orders, Guid>().Update(order);

            await _unitOfWork.CompleteAsync();

            var mappedOrder = _mapper.Map<OrderResultDto>(order);
            return mappedOrder;
        }

        public async Task<OrderResultDto> UpdateOrderPaymentSucceeded(string paymentIntentId)
        {
            var orderExist = await _unitOfWork.Repository<Orders, Guid>().GetIfExistsAsync(x => x.PaymentIntentId == paymentIntentId);

            var order = await _unitOfWork.Repository<Orders, Guid>().GetByIdAsync(orderExist.Id);

            if (order is null)
                throw new Exception("Order Does Not Exist");

            order.OrderPayementStatus = OrderPayementStatus.Received;

            _unitOfWork.Repository<Orders, Guid>().Update(order);

            await _unitOfWork.CompleteAsync();

            var mappedOrder = _mapper.Map<OrderResultDto>(order);

            return mappedOrder;
        }
    }
}

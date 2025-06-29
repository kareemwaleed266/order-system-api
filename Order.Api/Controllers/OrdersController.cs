using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Data.Entities.OrderEntities;
using Order.Service.Services.OrderService;
using Order.Service.Services.OrderService.Dto;

namespace Order.Api.Controllers
{
    public class OrdersController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrdersController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<ActionResult<OrderResultDto>> CreateNewOrder(List<OrderDto> input, string buyerEmail)
        {
            var order = await _orderService.CreateNewOrderAsync(input, buyerEmail);

            if (order is null)
                return BadRequest("Error on Creating Order");

            return Ok(order);
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<ActionResult<IReadOnlyList<OrderResultDto>>> GetAllOrdersDetails()
            => Ok(await _orderService.GetAllOrdersDetailsAsync());

        [HttpGet]
        public async Task<ActionResult<OrderResultDto>> GetDetailsOfSpecificOrder(Guid? id)
            => Ok(await _orderService.GetDetailsOfSpecificOrderAsync(id));

        [HttpPut("{orderId}/status")]
        //[Authorize(Roles = "Admin")]

        public async Task<ActionResult<OrderResultDto>> UpdateOrderStatus(Guid? orderId, OrderPayementStatus status)
        {
            var order = await _orderService.UpdateOrderStatusAsync(orderId, status);

            if (order is null)
                return BadRequest("Error on Updating Order Status");

            return Ok(order);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Order.Service.HandleResponses;
using Order.Service.Services.CustomerService;
using Order.Service.Services.CustomerService.Dto;
using Order.Service.Services.OrderService.Dto;

namespace Order.Api.Controllers
{
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost]
        public async Task<ActionResult<CustomerResultDto>> CreateNewCustomer(CustomerDto input)
        {
            var customer = await _customerService.CreateNewCustomerAsync(input);

            if (customer is null)
                return BadRequest(new Responses(400, "Error on Customer Creating"));

            return Ok(customer);
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderResultDto>>> GetAllOrdersForCustomer(Guid id)
        {
            var orders = await _customerService.GetAllOrdersForCustomerAsync(id);

            if (orders is null)
                return BadRequest(new Responses(400, "Orders Not Found"));

            return Ok(orders);
        }

    }
}

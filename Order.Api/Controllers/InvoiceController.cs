using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Order.Service.Services.InvoiceService;
using Order.Service.Services.InvoiceService.Dto;

namespace Order.Api.Controllers
{
    public class InvoiceController : BaseController
    {
        private readonly IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }


        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<InvoiceResultDto>>> GetAllInvoices()
            => Ok(await _invoiceService.GetAllInvoicesAsync());


        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<InvoiceResultDto>> GetInvoiceById(Guid? id)
             => Ok(await _invoiceService.GetInvoiceByIdAsync(id));
    }
}

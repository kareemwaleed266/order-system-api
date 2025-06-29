using Order.Service.Services.InvoiceService.Dto;

namespace Order.Service.Services.InvoiceService
{
    public interface IInvoiceService
    {
        Task<InvoiceResultDto> GetInvoiceByIdAsync(Guid? invoiceId);
        Task<IReadOnlyList<InvoiceResultDto>> GetAllInvoicesAsync();

    }
}

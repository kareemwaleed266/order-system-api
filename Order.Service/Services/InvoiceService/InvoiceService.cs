using AutoMapper;
using Order.Data.Entities;
using Order.Data.Entities.OrderEntities;
using Order.Repository.Interfaces;
using Order.Service.Services.InvoiceService.Dto;

namespace Order.Service.Services.InvoiceService
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public InvoiceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<IReadOnlyList<InvoiceResultDto>> GetAllInvoicesAsync()
        {
            var Invoices = await _unitOfWork.Repository<Invoice, Guid>().GetAllAsync();
            var mappedInvoices = new List<InvoiceResultDto>();

            foreach (var invoice in Invoices)
            {
                var order = await _unitOfWork.Repository<Orders, Guid>().GetByIdAsync(invoice.OrderId);
                if (order is null)
                    throw new Exception("Orders not found");
                var mappedInvoice = _mapper.Map<InvoiceResultDto>(invoice);
                mappedInvoice.Discount = order.Discount;
                mappedInvoices.Add(mappedInvoice);
            }

            return mappedInvoices;
        }

        public async Task<InvoiceResultDto> GetInvoiceByIdAsync(Guid? id)
        {
            if (id is null) throw new Exception("Id Is Null");

            var invoice = await _unitOfWork.Repository<Invoice, Guid>().GetByIdAsync(id.Value);

            if (invoice is null) throw new Exception("Invoice Not Found");

            var order = await _unitOfWork.Repository<Orders, Guid>().GetByIdAsync(invoice.OrderId);
            var mappedInvoice = _mapper.Map<InvoiceResultDto>(invoice);
            mappedInvoice.Discount = order.Discount;
            return mappedInvoice;
        }
    }
}

using AutoMapper;
using Order.Data.Entities;
using Order.Data.Entities.CustomerEntities;
using Order.Data.Entities.OrderEntities;
using Order.Data.Entities.ProductEntities;
using Order.Repository.Interfaces;
using Order.Service.Helper;
using Order.Service.Services.OrderService.Dto;
using Order.Service.Services.PaymentService;

namespace Order.Service.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPaymentService _paymentService;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper, IPaymentService paymentService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _paymentService = paymentService;
        }
        public async Task<OrderResultDto> CreateNewOrderAsync(List<OrderDto> input, string buyerEmail)
        {
            var customer = await _unitOfWork.Repository<Customer, Guid>().GetIfExistsAsync(x => x.Email == buyerEmail);
            if (customer == null)
                throw new Exception("Customer Not Found");

            var orderedItems = new List<OrderItemDto>();

            foreach (var orderDto in input)
            {
                //fill orderdto

                var productItem = await _unitOfWork.Repository<Product, int>().GetByIdAsync(orderDto.ProductId);
                if (productItem is null)
                    throw new Exception($"Product With Id {orderDto.ProductId} Not Exists");

                if (productItem.Stock < orderDto.Quantity)
                    orderDto.Quantity = productItem.Stock;

                orderDto.BuyerEmail = buyerEmail;

                //fill itemordered
                var itemordered = new ProductItemOrdered
                {
                    ProductItemId = orderDto.ProductId,
                    ProductItemName = productItem.Name
                };

                //fill orderItems
                var orderItem = new OrderItems
                {
                    UnitPrice = productItem.Price,
                    itemOrdered = itemordered,
                    Quantity = orderDto.Quantity
                };

                var mappedOrderItem = _mapper.Map<OrderItemDto>(orderItem);

                orderedItems.Add(mappedOrderItem);
            }

            var subTotal = orderedItems.Sum(item => item.Quantity * item.UnitPrice);
            decimal total = 0m;
            string discount = "0";
            if (subTotal > 100 && subTotal < 200)
            {
                total = subTotal - (subTotal * (5m / 100m));
                discount = "5 %";
            }
            if (subTotal > 200)

            {
                total = subTotal - (subTotal * (10m / 100m));
                discount = "10 %";
            }


            // =>to do after payment
            // =>to do after payment
            //var customer = await _unitOfWork.Repository<Customer, Guid>().GetByEmailAsync(buyerEmail);
            var mappedOrderItems = _mapper.Map<List<OrderItems>>(orderedItems);

            var order = new Orders
            {
                CustomerId = customer.Id,
                Discount = discount,
                PaymentIntentId = "",
                OrderItems = mappedOrderItems,
                TotalAmount = total
            };
            await _unitOfWork.Repository<Orders, Guid>().AddAsync(order);

            var orderBasket = new OrderBasketDto
            {
                CustomerId = customer.Id,
                OrderId = order.Id,
                OrderItems = mappedOrderItems,
                TotalAmount = total,
                PaymentIntentId = "",
            };

            var existingOrder = await _unitOfWork.Repository<Orders, Guid>().GetByIdAsync(orderBasket.OrderId);
            if (existingOrder != null)
            {
                _unitOfWork.Repository<Orders, Guid>().Delete(existingOrder);
                orderBasket = await _paymentService.CreateOrUpdatePaymentIntentForExistingOrder(orderBasket);
            }

            else
            {
                orderBasket = await _paymentService.CreateOrUpdatePaymentIntentForNewOrder(order.Id);
            }


            mappedOrderItems = _mapper.Map<List<OrderItems>>(orderedItems);

            order = new Orders
            {
                CustomerId = customer.Id,
                Discount = discount,
                PaymentIntentId = orderBasket.PaymentIntentId,
                OrderItems = mappedOrderItems,
                TotalAmount = total
            };
            await _unitOfWork.Repository<Orders, Guid>().AddAsync(order);

            var invoice = new Invoice
            {
                OrderId = order.Id,
                TotalAmount = total
            };
            await _unitOfWork.Repository<Invoice, Guid>().AddAsync(invoice);

            await _unitOfWork.CompleteAsync();

            var mappedOrder = _mapper.Map<OrderResultDto>(order);
            mappedOrder.Discount = discount;

            return mappedOrder;
        }

        public async Task<IReadOnlyList<OrderResultDto>> GetAllOrdersDetailsAsync()
        {
            var orders = await _unitOfWork.Repository<Orders, Guid>().GetAllAsync();

            if (orders is { Count: <= 0 })
                throw new Exception("You Do Not Have Orders Right Now");

            var mappedOrders = _mapper.Map<List<OrderResultDto>>(orders);

            return mappedOrders;
        }

        public async Task<OrderResultDto> GetDetailsOfSpecificOrderAsync(Guid? id)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            var OrderExist = await _unitOfWork.Repository<Orders, Guid>().ExistsAsync(x => x.Id == id);
            if (!OrderExist)
                throw new Exception("Order Not Found");

            var order = await _unitOfWork.Repository<Orders, Guid>().GetByIdAsync(id.Value);
            var mappedOrders = _mapper.Map<OrderResultDto>(order);
            return mappedOrders;
        }

        public async Task<OrderStatusResultDto> UpdateOrderStatusAsync(Guid? id, OrderPayementStatus status)
        {
            if (id == null)
                throw new ArgumentNullException("id");

            var order = await _unitOfWork.Repository<Orders, Guid>().GetByIdAsync(id.Value);

            if (order == null)
                throw new Exception("Order Not Found");

            order.OrderPayementStatus = status;
            var customer = await _unitOfWork.Repository<Customer, Guid>().GetByIdAsync(order.CustomerId);
            var customerEmail = new Email
            {
                Title = "Payment Status Updated",
                Body = $"Your Order Payment Status is Changed To {status}",
                To = customer.Email
            };

            EmailSettings.SendEmail(customerEmail);

            var mappedOrder = _mapper.Map<OrderStatusResultDto>(order);
            return mappedOrder;
        }
    }
}

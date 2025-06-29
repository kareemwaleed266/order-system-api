using Microsoft.AspNetCore.Mvc;
using Order.Repository.Interfaces;
using Order.Repository.Repositories;
using Order.Service.HandleResponses;
using Order.Service.Services.CustomerService;
using Order.Service.Services.CustomerService.Dto;
using Order.Service.Services.InvoiceService;
using Order.Service.Services.InvoiceService.Dto;
using Order.Service.Services.OrderService;
using Order.Service.Services.OrderService.Dto;
using Order.Service.Services.PaymentService;
using Order.Service.Services.ProductService;
using Order.Service.Services.ProductService.ProductDto;
using Order.Service.Services.TokenService;
using Order.Service.Services.UserService;

namespace Order.Api.Extensions
{
    public static class AppServiceExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddAutoMapper(typeof(ProductProfile));
            services.AddAutoMapper(typeof(OrderProfile));
            services.AddAutoMapper(typeof(CustomerProfile));
            services.AddAutoMapper(typeof(InvoiceProfile));

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                    .Where(model => model.Value.Errors.Count > 0)
                    .SelectMany(model => model.Value.Errors)
                    .Select(error => error.ErrorMessage)
                    .ToList();

                    var errorResponse = new ValidationErrorResponse
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(errorResponse);
                };
            });
            return services;
        }
    }
}

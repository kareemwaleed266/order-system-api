using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Order.Data.Entities.CustomerEntities;
using Order.Data.Entities.IdentityEntities;
using Order.Repository.Interfaces;
using Order.Service.Services.CustomerService.Dto;
using Order.Service.Services.OrderService.Dto;
using Order.Service.Services.TokenService;

namespace Order.Service.Services.CustomerService
{
    public class CustomerService : ICustomerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;

        public CustomerService(IUnitOfWork unitOfWork,
            IMapper mapper,
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            ITokenService tokenService
            )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<CustomerResultDto> CreateNewCustomerAsync(CustomerDto input)
        {
            if (input == null) throw new ArgumentNullException("input is Null");

            var customer = _mapper.Map<Customer>(input);

            var customerExist = await _unitOfWork.Repository<Customer, Guid>().ExistsAsync(x => x.Email.Equals(customer.Email));
            if (customerExist)
                throw new Exception("Customer Already Exist");

            await _unitOfWork.Repository<Customer, Guid>().AddAsync(customer);
            await _unitOfWork.CompleteAsync();

            var appUser = new AppUser
            {
                DisplayName = input.Name,
                Email = input.Email,
                NormalizedEmail = input.Email.ToUpper(),
                UserName = input.Email.ToLower().Split('@')[0],
                NormalizedUserName = input.Email.ToUpper().Split('@')[0],
            };

            var result = await _userManager.CreateAsync(appUser, $"#{input.Name}Random123");
            if (!result.Succeeded)
                throw new Exception("Error Registering Account");
            var mappedCustomer = _mapper.Map<CustomerResultDto>(input);
            mappedCustomer.UserName = input.Email.ToLower().Split('@')[0];

            mappedCustomer.Token = _tokenService.GenerateToken(appUser, "Customer");

            return mappedCustomer;
        }

        public async Task<IReadOnlyList<OrderResultDto>> GetAllOrdersForCustomerAsync(Guid? id)
        {
            if (id == null) throw new ArgumentNullException("id");

            var customer = await _unitOfWork.Repository<Customer, Guid>().GetByIdAsync(id.Value);
            if (customer == null) throw new ArgumentNullException("Customer Not Found");

            var orders = customer.Orders.ToList();
            if (orders is null || orders.Count == 0)
                throw new Exception("Order Is Null");

            var mappedOrders = _mapper.Map<List<OrderResultDto>>(orders);

            return mappedOrders;
        }
    }
}
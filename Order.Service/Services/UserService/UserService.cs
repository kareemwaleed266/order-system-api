using Microsoft.AspNetCore.Identity;
using Order.Data.Entities.CustomerEntities;
using Order.Data.Entities.IdentityEntities;
using Order.Repository.Interfaces;
using Order.Service.Services.CustomerService;
using Order.Service.Services.TokenService;
using Order.Service.Services.UserService.Dto;

namespace Order.Service.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly ICustomerService _customerService;
        private readonly IUnitOfWork _unitOfWork;

        public UserService(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenService tokenService,
            ICustomerService customerService,
            IUnitOfWork unitOfWork
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _customerService = customerService;
            _unitOfWork = unitOfWork;
        }
        public async Task<UserDto> Login(LoginDto input)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);
            if (user is null)
                return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, input.Password, false);
            if (!result.Succeeded)
                throw new Exception("Login Failed");

            var adminRole = await _userManager.IsInRoleAsync(user, "Admin");
            var role = "";
            if (!adminRole)
                role = "Customer";
            role = "Admin";

            return new UserDto
            {
                Email = input.Email,
                DisplayName = user.DisplayName,
                Token = _tokenService.GenerateToken(user, role)
            };
        }

        public async Task<UserDto> Register(RegisterDto input)
        {
            var user = await _userManager.FindByEmailAsync(input.Email);

            if (user is not null)
                throw new Exception("Email Already Exists");

            var appUser = new AppUser
            {
                DisplayName = input.DisplayName,
                Email = input.Email,
                NormalizedEmail = input.Email.ToUpper(),
                UserName = input.Email.ToLower().Split('@')[0],
                NormalizedUserName = input.Email.ToUpper().Split('@')[0],
            };
            var result = await _userManager.CreateAsync(appUser, input.Password);

            if (!result.Succeeded)
                throw new Exception("Register Failed");

            var customer = new Customer
            {
                Email = appUser.Email,
                Name = input.DisplayName,
            };
            await _unitOfWork.Repository<Customer, Guid>().AddAsync(customer);
            await _unitOfWork.CompleteAsync();

            await _userManager.AddToRoleAsync(appUser, "Customer");

            var adminRole = await _userManager.IsInRoleAsync(appUser, "Admin");
            var role = "";
            if (!adminRole)
                role = "Customer";
            role = "Admin";
            return new UserDto
            {
                Email = input.Email,
                DisplayName = input.DisplayName,
                Token = _tokenService.GenerateToken(appUser, role)
            };
        }
    }
}

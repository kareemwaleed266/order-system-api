using Order.Data.Entities.IdentityEntities;

namespace Order.Service.Services.TokenService
{
    public interface ITokenService
    {
        string GenerateToken(AppUser appUser, string roles);
    }
}

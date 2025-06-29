using Microsoft.AspNetCore.Identity;

namespace Order.Data.Entities.IdentityEntities
{
    public class UserRoles : IdentityRole
    {
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;
    }
}

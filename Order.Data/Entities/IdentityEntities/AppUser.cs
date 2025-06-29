using Microsoft.AspNetCore.Identity;

namespace Order.Data.Entities.IdentityEntities
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

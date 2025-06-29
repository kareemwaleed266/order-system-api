using System.ComponentModel.DataAnnotations;

namespace Order.Service.Services.CustomerService.Dto
{
    public class CustomerDto
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Name { get; set; }
    }
}

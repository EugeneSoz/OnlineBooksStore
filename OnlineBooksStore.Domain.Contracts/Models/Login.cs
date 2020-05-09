using System.ComponentModel.DataAnnotations;

namespace OnlineBooksStore.Domain.Contracts.Models
{
    public class Login
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

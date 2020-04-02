using System.ComponentModel.DataAnnotations;

namespace OnlineBooksStore.App.WebApi.Models
{
    public class Login
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

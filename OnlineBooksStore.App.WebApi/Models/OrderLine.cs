using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace OnlineBooksStore.App.WebApi.Models
{
    public class OrderLine
    {
        [BindNever]
        public long OrderLineId { get; set; }
        [Required]
        public long ProductId { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}

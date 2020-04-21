using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using OnlineBooksStore.Domain.Contracts.Models.Orders;

namespace OnlineBooksStore.App.Contracts.Command
{
    public abstract class OrderCommand : Command
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public bool Shipped { get; set; }
        [Required]
        public IEnumerable<OrderLine> Lines { get; set; }
    }

    public sealed class CreateOrderCommand : OrderCommand { }
}
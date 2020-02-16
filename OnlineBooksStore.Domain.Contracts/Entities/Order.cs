using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBooksStore.Domain.Contracts.Entities
{
    public class Order : EntityBase
    {
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public bool Shipped { get; set; }
        public IEnumerable<OrderLine> Lines { get; set; }
    }
}

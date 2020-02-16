namespace OnlineBooksStore.Domain.Contracts.Entities
{
    public class OrderLine : EntityBase
    {
        public long BookId { get; set; }
        public Book Book { get; set; }
        public int Quantity { get; set; }
        public long OrderId { get; set; }
        public Order Order { get; set; }
    }
}
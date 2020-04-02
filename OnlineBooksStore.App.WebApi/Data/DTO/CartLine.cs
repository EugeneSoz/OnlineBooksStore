namespace OnlineBooksStore.App.WebApi.Data.DTO
{
    public class CartLine
    {
        public long ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}

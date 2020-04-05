namespace OnlineBooksStore.Domain.Contracts.Models
{
    public class BookResponse
    {
        public long Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Authors { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Language { get; set; } = string.Empty;
        public int PageCount { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string BookCover { get; set; }
        public long? CategoryID { get; set; }
        public long? PublisherID { get; set; }
        public string CategoryName { get; set; }
        public string SubcategoryName { get; set; }
        public string PublisherName { get; set; }
    }
}

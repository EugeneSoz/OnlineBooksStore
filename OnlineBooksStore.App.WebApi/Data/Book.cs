using OnlineBooksStore.App.WebApi.Data.DTO;

namespace OnlineBooksStore.App.WebApi.Data
{
    //класс, описывающий книгу
    public class Book : BookDTO
    {
        public Category Category { get; set; }
        public Publisher Publisher { get; set; }
    }
}

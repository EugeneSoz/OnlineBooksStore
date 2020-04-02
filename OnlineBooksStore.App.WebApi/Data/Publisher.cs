using System.Collections.Generic;
using OnlineBooksStore.App.WebApi.Data.DTO;

namespace OnlineBooksStore.App.WebApi.Data
{
    public class Publisher : PublisherDTO
    {
        public List<Book> Books { get; set; }
    }
}

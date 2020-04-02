using System.Collections.Generic;
using OnlineBooksStore.App.WebApi.Data;

namespace OnlineBooksStore.App.WebApi.Models
{
    public class StoreSavedData
    {
        public List<Publisher> Publishers { get; set; }
        public List<Category> Categories { get; set; }
        public List<Category> ParentCategories { get; set; }
        public List<Book> Books { get; set; }
    }
}

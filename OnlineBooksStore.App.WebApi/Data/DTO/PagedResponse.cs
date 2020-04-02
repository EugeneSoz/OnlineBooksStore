using System.Collections.Generic;
using OnlineBooksStore.App.WebApi.Models;

namespace OnlineBooksStore.App.WebApi.Data.DTO
{
    public class PagedResponse<T>
    {
        public List<T> Entities { get; set; }
        public Pagination Pagination { get; set; }
        public List<int> PageNumbers { get; set; }
    }
}

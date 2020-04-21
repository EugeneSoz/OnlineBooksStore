using System.Collections.Generic;

namespace OnlineBooksStore.Domain.Contracts.Models.Pages
{
    public class PagedResponse<T>
    {
        public List<T> Entities { get; set; }
        public Pagination Pagination { get; set; }
        public List<int> PageNumbers { get; set; }
    }
}

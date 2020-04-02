using System.Collections.Generic;
using OnlineBooksStore.App.WebApi.Data.DTO;

namespace OnlineBooksStore.App.WebApi.Data
{
    public class Category : CategoryDTO
    {
        //для сортировки по имени родительской и текущей категории
        public string DisplayedName { get; set; }
        public Category ParentCategory { get; set; }
        public List<Category> ChildrenCategories { get; set; }
        public List<Book> Books { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using OnlineBooksStore.Domain.Contracts.Entities;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Pages;

namespace OnlineBooksStore.Domain.Contracts.Repositories
{
    public interface ICategoriesRepository
    {
        IEnumerable<Category> Categories { get; }
        PagedList<Category> GetCategories(QueryOptions options);
        Category GetCategory(long id);
        List<StoreCategoryResponse> GetStoreCategories();
        List<Category> GetParentCategories();
        Category AddCategory(Category category);
        bool UpdateCategory(Category category);
        bool DeleteCategory(Category category);
    }
}

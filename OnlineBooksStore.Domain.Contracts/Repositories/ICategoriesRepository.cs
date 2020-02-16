using System;
using System.Collections.Generic;
using System.Text;
using OnlineBooksStore.Domain.Contracts.Entities;
using OnlineBooksStore.Domain.Contracts.Models.Pages;

namespace OnlineBooksStore.Domain.Contracts.Repositories
{
    public interface ICategoriesRepository
    {
        IEnumerable<Category> Categories { get; }
        PagedList<Category> GetCategories(QueryOptions options);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}

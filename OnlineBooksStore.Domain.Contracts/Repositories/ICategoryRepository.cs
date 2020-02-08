using System;
using System.Collections.Generic;
using System.Text;
using OnlineBooksStore.Domain.Contracts.Entities;

namespace OnlineBooksStore.Domain.Contracts.Repositories
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}

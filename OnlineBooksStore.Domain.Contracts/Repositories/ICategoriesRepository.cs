using System.Collections.Generic;
using OnlineBooksStore.Domain.Contracts.Models.Category;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Persistence.Entities;

namespace OnlineBooksStore.Domain.Contracts.Repositories
{
    public interface ICategoriesRepository
    {
        PagedList<CategoryEntity> GetCategories(QueryOptions options);
        CategoryEntity GetCategory(long id);
        List<CategoryEntity> GetStoreCategories();
        List<CategoryEntity> GetParentCategories();
        CategoryEntity AddCategory(CategoryEntity category);
        bool UpdateCategory(CategoryEntity category);
        bool DeleteChildrenCategories(long parentId);
        bool DeleteCategory(CategoryEntity category);
    }
}

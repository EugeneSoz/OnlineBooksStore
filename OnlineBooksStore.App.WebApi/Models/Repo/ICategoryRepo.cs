using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineBooksStore.App.WebApi.Data;
using OnlineBooksStore.App.WebApi.Data.DTO;

namespace OnlineBooksStore.App.WebApi.Models.Repo
{
    public interface ICategoryRepo : IBaseRepo<Category>
    {
        Task<Category> GetCategoryAsync(long id);
        Task<PagedList<CategoryResponse>> GetCategoriesAsync(QueryOptions options);
        Task<List<StoreCategoryResponse>> GetStoreCategoriesAsync();
        Task<List<Category>> GetParentCategoriesAsync();
        Task<bool> DeleteAsync(long parentCategoryId);
    }
}

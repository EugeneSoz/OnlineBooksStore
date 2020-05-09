using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineBooksStore.App.Contracts.Command;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.Domain.Contracts.Models.Categories;
using OnlineBooksStore.Domain.Contracts.Models.Pages;

namespace OnlineBooksStore.Integration.Contracts.Admin
{
    public interface ICategoryClientService
    {
        Task<Category> GetCategoryAsync(CategoryIdQuery query);
        Task<PagedResponse<CategoryResponse>> GetCategoriesAsync(PageFilterQuery query);
        Task<List<Category>> GetParentCategoriesAsync(ParentCategoryCategoryQuery query);
        Task<List<CategoryResponse>> GetCategoriesForSelectionAsync(SearchTermQuery query);
        Task CreateCategoryAsync(CreateCategoryCommand command);
        Task UpdateCategoryAsync(UpdateCategoryCommand command);
        Task DeleteCategoryAsync(DeleteCategoryCommand command);
    }
}
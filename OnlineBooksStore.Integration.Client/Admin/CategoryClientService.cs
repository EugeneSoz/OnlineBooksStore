using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using OnlineBooksStore.App.Contracts.Command;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.Domain.Contracts.Models.Categories;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Integration.Contracts.Admin;

namespace OnlineBooksStore.Integration.Client.Admin
{
    public class CategoryClientService : RestService, ICategoryClientService
    {
        private const string Controller = "api/Category";
        public CategoryClientService(IHttpClientFactory httpClientFactory) : base(httpClientFactory) { }

        public async Task<Category> GetCategoryAsync(CategoryIdQuery query)
        {
            return await GetJsonAsync<Category>($"{Controller}/{query.Id}");
        }

        public async Task<PagedResponse<CategoryResponse>> GetCategoriesAsync(PageFilterQuery query)
        {
            return await PostJsonAsync<PagedResponse<CategoryResponse>>($"{Controller}/categories", query);
        }

        public async Task<List<Category>> GetParentCategoriesAsync(ParentCategoryCategoryQuery query)
        {
            return await GetJsonAsync<List<Category>>($"{Controller}/parentcategories");
        }

        public async Task<List<CategoryResponse>> GetCategoriesForSelectionAsync(SearchTermQuery query)
        {
            return await PostJsonAsync<List<CategoryResponse>>($"{Controller}/categoriesforselection", query);
        }

        public async Task CreateCategoryAsync(CreateCategoryCommand command)
        {
            await PostJsonAsync<bool>($"{Controller}/create", command);
        }

        public async Task UpdateCategoryAsync(UpdateCategoryCommand command)
        {
            await PostJsonAsync<bool>($"{Controller}/update", command);
        }

        public async Task DeleteCategoryAsync(DeleteCategoryCommand command)
        {
            await PostJsonAsync<bool>($"{Controller}/delete", command);
        }
    }
}
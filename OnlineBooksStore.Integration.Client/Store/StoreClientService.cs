using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.Domain.Contracts.Models.Books;
using OnlineBooksStore.Domain.Contracts.Models.Categories;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Integration.Contracts.Store;

namespace OnlineBooksStore.Integration.Client.Store
{
    public class StoreClientService : RestService, IStoreClientService
    {
        private const string Controller = "api/Store";
        public StoreClientService(IHttpClientFactory httpClientFactory) : base(httpClientFactory) { }

        public async Task<BookResponse> GetBookAsync(BookIdQuery query)
        {
            return await GetJsonAsync<BookResponse>($"{Controller}/book/{query.Id}");
        }

        public async Task<PagedResponse<BookResponse>> GetBooksAsync(PageFilterQuery query)
        {
            return await PostJsonAsync<PagedResponse<BookResponse>>($"{Controller}/books", query);
        }

        public async Task<List<StoreCategoryResponse>> GetStoreCategoriesAsync(StoreCategoryQuery query)
        {
            return await GetJsonAsync<List<StoreCategoryResponse>>($"{Controller}/categories");
        }
    }
}
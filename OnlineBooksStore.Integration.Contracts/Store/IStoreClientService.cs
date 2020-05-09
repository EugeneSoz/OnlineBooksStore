using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.Domain.Contracts.Models.Books;
using OnlineBooksStore.Domain.Contracts.Models.Categories;
using OnlineBooksStore.Domain.Contracts.Models.Pages;

namespace OnlineBooksStore.Integration.Contracts.Store
{
    public interface IStoreClientService
    {
        Task<BookResponse> GetBookAsync(BookIdQuery query);
        Task<PagedResponse<BookResponse>> GetBooksAsync(PageFilterQuery query);
        Task<List<StoreCategoryResponse>> GetStoreCategoriesAsync(StoreCategoryQuery query);
    }
}
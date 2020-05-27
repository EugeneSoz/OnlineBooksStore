using System.Net.Http;
using System.Threading.Tasks;
using OnlineBooksStore.App.Contracts.Command;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.Domain.Contracts.Models.Books;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Integration.Contracts.Admin;

namespace OnlineBooksStore.Integration.Client.Admin
{
    public class BookClientService : RestService, IBookClientService
    {
        private const string Controller = "api/Book";
        public BookClientService(IHttpClientFactory httpClientFactory) : base(httpClientFactory) { }

        public async Task<BookResponse> GetBookAsync(BookIdQuery query)
        {
            return await GetJsonAsync<BookResponse>($"{Controller}/book/{query.Id}");
        }

        public async Task<PagedResponse<BookResponse>> GetBooksAsync(PageFilterQuery query)
        {
            return await PostJsonAsync<PagedResponse<BookResponse>>($"{Controller}/books", query);
        }

        public async Task CreateBookAsync(CreateBookCommand command)
        {
            await PostJsonAsync<bool>($"{Controller}/create", command);
        }

        public async Task UpdateBookAsync(UpdateBookCommand command)
        {
            await PostJsonAsync<bool>($"{Controller}/update", command);
        }

        public async Task DeleteBookAsync(DeleteBookCommand command)
        {
            await PostJsonAsync<bool>($"{Controller}/delete", command);
        }
    }
}
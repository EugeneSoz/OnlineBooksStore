using System.Threading.Tasks;
using OnlineBooksStore.App.Contracts.Command;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.Domain.Contracts.Models.Books;
using OnlineBooksStore.Domain.Contracts.Models.Pages;

namespace OnlineBooksStore.Integration.Contracts.Admin
{
    public interface IBookClientService
    {
        Task<BookResponse> GetBookAsync(BookIdQuery query);
        Task<PagedResponse<BookResponse>> GetBooksAsync(PageFilterQuery query);
        Task CreateBookAsync(CreateBookCommand command);
        Task UpdateBookAsync(UpdateBookCommand command);
        Task DeleteBookAsync(DeleteBookCommand command);
    }
}
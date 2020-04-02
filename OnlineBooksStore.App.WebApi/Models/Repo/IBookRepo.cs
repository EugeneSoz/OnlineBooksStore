using System.Threading.Tasks;
using OnlineBooksStore.App.WebApi.Data;
using OnlineBooksStore.App.WebApi.Data.DTO;

namespace OnlineBooksStore.App.WebApi.Models.Repo
{
    public interface IBookRepo : IBaseRepo<Book>
    {
        Task<BookResponse> GetBookAsync(long id);
        Task<PagedList<BookResponse>> GetBooksAsync(QueryOptions options);
    }
}

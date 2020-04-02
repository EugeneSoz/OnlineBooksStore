using System.Threading.Tasks;
using OnlineBooksStore.App.WebApi.Data;
using OnlineBooksStore.App.WebApi.Data.DTO;

namespace OnlineBooksStore.App.WebApi.Models.Repo
{
    public interface IPublisherRepo : IBaseRepo<Publisher>
    {
        Task<Publisher> GetPublisherAsync(long id);
        Task<PagedList<Publisher>> GetPublishersAsync(QueryOptions options);
    }
}

using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnlineBooksStore.App.WebApi.Data;
using OnlineBooksStore.App.WebApi.Data.DTO;

namespace OnlineBooksStore.App.WebApi.Models.Repo
{
    public class PublisherRepo : BaseRepo<Publisher>, IPublisherRepo
    {
        public PublisherRepo(StoreDbContext ctx) : base(ctx) { }

        public async Task<Publisher> GetPublisherAsync(long id)
        {
            IQueryable<Publisher> publishers = GetEntities();
            IQueryable<Book> books = Context.Books.OrderBy(b => b.Title);

            Publisher result = await publishers
                .GroupJoin(books, p => p.Id, b => b.PublisherID, (p, relbooks) =>
                new Publisher
                {
                    Id = p.Id,
                    Name = p.Name,
                    Country = p.Country,
                    Books = relbooks.ToList()
                })
                .SingleOrDefaultAsync(p => p.Id == id);

            return result;
        }

        public async Task<PagedList<Publisher>> GetPublishersAsync(QueryOptions options)
        {
            QueryProcessing<Publisher> processing = new QueryProcessing<Publisher>(options);

            IQueryable<Publisher> query = GetEntities();
            IQueryable<Publisher> publishers = processing.ProcessQuery(query);

            return await Task.Run(() => new PagedList<Publisher>(publishers, options));
        }
    }
}

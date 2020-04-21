using System.Linq;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Domain.Contracts.Repositories;
using OnlineBooksStore.Persistence.Entities;

namespace OnlineBooksStore.Persistence.EF
{
    public class PublishersRepository : BaseRepo<PublisherEntity>, IPublishersRepository
    {
        public PublishersRepository(StoreDbContext ctx) : base(ctx) { }

        public PagedList<PublisherEntity> GetPublishers(QueryOptions options)
        {
            var processing = new QueryProcessing<PublisherEntity>(options);

            IQueryable<PublisherEntity> query = GetEntities();
            IQueryable<PublisherEntity> publishers = processing.ProcessQuery(query);

            return new PagedList<PublisherEntity>(publishers, options);
        }

        public PublisherEntity GetPublisher(long id)
        {
            IQueryable<PublisherEntity> publishers = GetEntities();
            var books = Context.Books
                .Where(b => b.PublisherId == id)
                .OrderBy(b => b.Title)
                .Select(b => new BookEntity
                {
                    Id = b.Id,
                    Authors = b.Authors,
                    Title = b.Title,
                    PurchasePrice = b.PurchasePrice,
                    RetailPrice = b.RetailPrice

                });

            var publisher = publishers.SingleOrDefault(p => p.Id == id);
            if (publisher != null)
            {
                publisher.Books = books.ToList();
            }

            return publisher;
        }

        public PublisherEntity AddPublisher(PublisherEntity publisher)
        {
            return Add(publisher);
        }

        public bool UpdatePublisher(PublisherEntity publisher)
        {
            return Update(publisher);
        }

        public bool DeletePublisher(PublisherEntity publisher)
        {
            return Delete(publisher);
        }
    }
}

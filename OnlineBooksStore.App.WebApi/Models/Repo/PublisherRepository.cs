using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineBooksStore.App.WebApi.Data;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Domain.Contracts.Repositories;
using Publisher = OnlineBooksStore.Domain.Contracts.Models.Publisher;

namespace OnlineBooksStore.App.WebApi.Models.Repo
{
    public class PublisherRepository : BaseRepo<Publisher>, IPublishersRepository
    {
        public PublisherRepository(StoreDbContext ctx) : base(ctx) { }
        public IEnumerable<Publisher> Publishers { get; }
        public Domain.Contracts.Models.Pages.PagedList<Publisher> GetPublishers(QueryOptions options)
        {
            QueryProcessing<Publisher> processing = new QueryProcessing<Publisher>(options);

            IQueryable<Publisher> query = GetEntities();
            IQueryable<Publisher> publishers = processing.ProcessQuery(query);

            return new Domain.Contracts.Models.Pages.PagedList<Publisher>(publishers, options);
        }

        public Publisher GetPublisher(long id)
        {
            IQueryable<Publisher> publishers = GetEntities();
            var books = Context.Books
                .Where(b => b.PublisherId == id)
                .OrderBy(b => b.Title)
                .Select(b => new RelatedBook
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

        public Publisher AddPublisher(Publisher publisher)
        {
            return Add(publisher);
        }

        public bool UpdatePublisher(Publisher publisher)
        {
            return Update(publisher);
        }

        public bool DeletePublisher(Publisher publisher)
        {
            return Delete(publisher);
        }
    }
}

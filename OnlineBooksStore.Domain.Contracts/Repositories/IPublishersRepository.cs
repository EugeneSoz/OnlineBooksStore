using System.Collections.Generic;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Pages;

namespace OnlineBooksStore.Domain.Contracts.Repositories
{
    public interface IPublishersRepository
    {
        IEnumerable<Publisher> Publishers { get; }
        PagedList<Publisher> GetPublishers(QueryOptions options);
        Publisher GetPublisher(long id);
        Publisher AddPublisher(Publisher publisher);
        bool UpdatePublisher(Publisher publisher);
        bool DeletePublisher(Publisher publisher);
    }
}
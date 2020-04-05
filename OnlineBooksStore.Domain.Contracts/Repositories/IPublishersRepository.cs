using System.Collections.Generic;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Persistence.Entities;

namespace OnlineBooksStore.Domain.Contracts.Repositories
{
    public interface IPublishersRepository
    {
        PagedList<PublisherEntity> GetPublishers(QueryOptions options);
        PublisherEntity GetPublisher(long id);
        PublisherEntity AddPublisher(PublisherEntity publisher);
        bool UpdatePublisher(PublisherEntity publisher);
        bool DeletePublisher(PublisherEntity publisher);
    }
}
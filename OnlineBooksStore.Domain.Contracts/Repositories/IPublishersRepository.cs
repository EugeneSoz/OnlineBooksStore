using System.Collections.Generic;
using OnlineBooksStore.Domain.Contracts.Entities;
using OnlineBooksStore.Domain.Contracts.Models.Pages;

namespace OnlineBooksStore.Domain.Contracts.Repositories
{
    public interface IPublishersRepository
    {
        IEnumerable<Publisher> Publishers { get; }
        PagedList<Publisher> GetPublishers(QueryOptions options);
        void AddPublisher(Publisher publisher);
        void UpdatePublisher(Publisher publisher);
        void DeletePublisher(Publisher publisher);
    }
}
using System.Collections.Generic;
using OnlineBooksStore.Domain.Contracts.Entities;

namespace OnlineBooksStore.Domain.Contracts.Repositories
{
    public interface IPublishersRepository
    {
        IEnumerable<Publisher> Publishers { get; }
        void AddPublisher(Publisher publisher);
        void UpdatePublisher(Publisher publisher);
        void DeletePublisher(Publisher publisher);
    }
}
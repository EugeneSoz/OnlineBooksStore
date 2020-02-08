using System;
using System.Collections.Generic;
using System.Text;
using OnlineBooksStore.Domain.Contracts.Entities;

namespace OnlineBooksStore.Domain.Contracts.Repositories
{
    public interface IPublisherRepository
    {
        IEnumerable<Publisher> Publishers { get; }
        void AddPublisher(Publisher publisher);
        void UpdatePublisher(Publisher publisher);
        void DeletePublisher(Publisher publisher);
    }
}

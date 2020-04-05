using System;
using System.Collections.Generic;
using OnlineBooksStore.Domain.Contracts.Entities;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Domain.Contracts.Models.Publishers;
using OnlineBooksStore.Domain.Contracts.Repositories;

namespace OnlineBooksStore.Persistence.EF.Mvc
{
    public class PublishersRepository : IPublishersRepository
    {
        private readonly DataContext _context;

        public PublishersRepository(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IEnumerable<Publisher> Publishers => _context.Publishers;

        public PagedList<Publisher> GetPublishers(QueryOptions options)
        {
            return new PagedList<Publisher>(_context.Publishers, options);
        }

        public void AddPublisher(Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            _context.SaveChanges();
        }

        public void UpdatePublisher(Publisher publisher)
        {
            _context.Publishers.Update(publisher);
            _context.SaveChanges();
        }

        public void DeletePublisher(Publisher publisher)
        {
            _context.Publishers.Remove(publisher);
            _context.SaveChanges();
        }
    }
}

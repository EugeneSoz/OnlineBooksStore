using OnlineBooksStore.Domain.Contracts.Entities;
using OnlineBooksStore.Domain.Contracts.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OnlineBooksStore.Persistence.EF
{
    public class DataRepository : IRepository
    {
        private readonly DataContext _context;

        public DataRepository(DataContext context)
        {
            _context = context;
        }
        public IEnumerable<Book> Books => _context.Books.ToArray();

        public Book GetBook(long key) => _context.Books.Find(key);

        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            var savedBook = GetBook(book.Id);
            savedBook.Title = book.Title;
            savedBook.Category = book.Category;
            savedBook.Publisher = book.Publisher;
            savedBook.PurchasePrice = book.PurchasePrice;
            savedBook.RetailPrice = book.RetailPrice;

            _context.SaveChanges();
        }

        public void UpdateAll(Book[] books)
        {
            var data = books.ToDictionary(b => b.Id);
            var baseline = _context.Books.Where(b => data.Keys.Contains(b.Id));

            foreach (var databaseBook in baseline)
            {
                var requestBook = data[databaseBook.Id];
                databaseBook.Title = requestBook.Title;
                databaseBook.Category = requestBook.Category;
                databaseBook.Publisher = requestBook.Publisher;
                databaseBook.PurchasePrice = requestBook.PurchasePrice;
                databaseBook.RetailPrice = requestBook.RetailPrice;
            }

            _context.SaveChanges();
        }

        public void Delete(Book book)
        {
            _context.Books.Remove(book);

            _context.SaveChanges();
        }
    }
}

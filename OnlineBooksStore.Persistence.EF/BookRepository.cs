using OnlineBooksStore.Domain.Contracts.Entities;
using OnlineBooksStore.Domain.Contracts.Repositories;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace OnlineBooksStore.Persistence.EF
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;

        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public IEnumerable<Book> Books => _context.Books
            .Include(b => b.Category)
            .Include(b => b.Publisher);

        public Book GetBook(long key)
        {
            return _context.Books.Include(b => b.Category)
                .Include(b => b.Publisher).First(b => b.Id == key);
        }

        public void AddBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            var savedBook = _context.Books.Find(book.Id);
            savedBook.Title = book.Title;
            savedBook.CategoryID = book.CategoryID;
            savedBook.PublisherID = book.PublisherID;
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

using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineBooksStore.Domain.Contracts.Entities;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Repositories;

namespace OnlineBooksStore.Persistence.EF.Mvc
{
    public class WebServiceRepository : IWebServiceRepository
    {
        private readonly DataContext _context;

        public WebServiceRepository(DataContext context)
        {
            _context = context;
        }

        public object GetBook(long id)
        {
            return _context.Books
                .Include(book => book.Category)
                .Select(book => new 
                {
                    book.Id,
                    book.Title,
                    book.PurchasePrice,
                    book.RetailPrice,
                    book.CategoryId,
                    Category = new
                    {
                        book.Category.Id,
                        book.Category.NameEng
                    }
                })
                .FirstOrDefault(book => book.Id == id);
        }

        public object GetBooks(int skip, int take)
        {
            return _context.Books
                .Include(book => book.Category)
                .OrderBy(book => book.Id)
                .Skip(skip)
                .Take(take)
                .Select(book => new
                {
                    book.Id,
                    book.Title,
                    book.PurchasePrice,
                    book.RetailPrice,
                    book.CategoryId,
                    Category = new
                    {
                        book.Category.Id,
                        book.Category.NameEng
                    }
                });
        }

        public long StoreBook(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
            return book.Id;
        }

        public void UpdateBook(Book book)
        {
            _context.Books.Update(book);
            _context.SaveChanges();
        }

        public void DeleteBook(long id)
        {
            _context.Books.Remove(new Book {Id = id});
            _context.SaveChanges();
        }
    }
}
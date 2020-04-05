using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Category;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Domain.Contracts.Models.Publisher;
using OnlineBooksStore.Domain.Contracts.Repositories;
using Book = OnlineBooksStore.Domain.Contracts.Models.Book;

namespace OnlineBooksStore.Persistence.EF
{
    public class BooksRepository : BaseRepo<Book>, IBooksRepository
    {
        public BooksRepository(StoreDbContext ctx) : base(ctx) { }
        
        public IEnumerable<Book> Books { get; }
        public PagedList<Book> GetBooks(QueryOptions options)
        {
            QueryProcessing<Book> processing = new QueryProcessing<Book>(options);

            IQueryable<Book> query = GetEntities()
                .Include(p => p.Publisher)
                .Include(c => c.Category)
                .ThenInclude(c => c.ParentCategory);

            IQueryable<BookResponse> processedBooks;

            if (options.SortPropertyName == $"{nameof(Publisher)}.{nameof(Publisher.Name)}" ||
                options.SortPropertyName == $"{nameof(Category)}.{nameof(Category.Name)}" ||
                options.SortPropertyName == 
                $"{nameof(Category)}.{nameof(Category.ParentCategory)}.{nameof(Category.Name)}")
            {
                processedBooks = options.DescendingOrder
                    ? processing.ProcessQuery(query.OrderByDescending(b => b.Title))
                        .Select(e => e.MapBookResponse())
                    : processing.ProcessQuery(query.OrderBy(b => b.Title))
                        .Select(e => e.MapBookResponse());
            }
            else
            {
                processedBooks = processing.ProcessQuery(query)
                    .Select(e => e.MapBookResponse());
            }

            PagedList<BookResponse> books = new PagedList<BookResponse>(processedBooks, options);

            return books;

           
        }

        public Book GetBook(long key)
        {
            IQueryable<Book> entities = GetEntities();

            IQueryable<Book> book = entities
                .Include(p => p.Publisher)
                .Include(c => c.Category)
                .ThenInclude(c => c.ParentCategory);

            Book result = book.SingleOrDefault(b => b.Id == key);

            return result.MapBookResponse();
        }

        public void AddBook(Book book)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateBook(Book book)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Book book)
        {
            throw new System.NotImplementedException();
        }
    }
}

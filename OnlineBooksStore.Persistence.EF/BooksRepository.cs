using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using OnlineBooksStore.Domain.Contracts.Models.Categories;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Domain.Contracts.Models.Publishers;
using OnlineBooksStore.Domain.Contracts.Repositories;
using OnlineBooksStore.Persistence.Entities;

namespace OnlineBooksStore.Persistence.EF
{
    public class BooksRepository : BaseRepo<BookEntity>, IBooksRepository
    {
        public BooksRepository(StoreDbContext ctx) : base(ctx) { }
        
        public PagedList<BookEntity> GetBooks(QueryOptions options)
        {
            QueryProcessing<BookEntity> processing = new QueryProcessing<BookEntity>(options);

            IQueryable<BookEntity> query = GetEntities()
                .Include(p => p.Publisher)
                .Include(c => c.Category)
                .ThenInclude(c => c.ParentCategory);

            IQueryable<BookEntity> processedBooks;

            if (options.SortPropertyName == $"{nameof(Publisher)}.{nameof(Publisher.Name)}" ||
                options.SortPropertyName == $"{nameof(Category)}.{nameof(Category.Name)}" ||
                options.SortPropertyName == 
                $"{nameof(Category)}.{nameof(Category.ParentCategory)}.{nameof(Category.Name)}")
            {
                processedBooks = options.DescendingOrder
                    ? processing.ProcessQuery(query.OrderByDescending(b => b.Title))
                    : processing.ProcessQuery(query.OrderBy(b => b.Title));
            }
            else
            {
                processedBooks = processing.ProcessQuery(query);
            }

            var booksPagedList = new PagedList<BookEntity>(processedBooks, options);

            return booksPagedList;

           
        }

        public IEnumerable<BookEntity> GetSomeBooks(IEnumerable<long> booksIds)
        {
            var books = GetEntities().Where(b => booksIds.Contains(b.Id));

            return books.AsEnumerable();
        }

        public BookEntity GetBook(long key)
        {
            IQueryable<BookEntity> entities = GetEntities();

            IQueryable<BookEntity> book = entities
                .Include(p => p.Publisher)
                .Include(c => c.Category)
                .ThenInclude(c => c.ParentCategory);

            BookEntity result = book.SingleOrDefault(b => b.Id == key);

            return result;
        }

        public BookEntity AddBook(BookEntity book)
        {
            return Add(book);
        }

        public bool UpdateBook(BookEntity book)
        {
            return Update(book);
        }

        public bool DeleteBook(BookEntity book)
        {
            return Delete(book);
        }
    }
}

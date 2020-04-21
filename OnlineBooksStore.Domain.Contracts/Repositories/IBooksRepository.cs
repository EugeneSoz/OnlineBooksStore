using System.Collections.Generic;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Persistence.Entities;

namespace OnlineBooksStore.Domain.Contracts.Repositories
{
    public interface IBooksRepository
    {
        PagedList<BookEntity> GetBooks(QueryOptions options);
        IEnumerable<BookEntity> GetSomeBooks(IEnumerable<long> booksIds);
        BookEntity GetBook(long key);
        BookEntity AddBook(BookEntity book);
        bool UpdateBook(BookEntity book);
        bool Delete(BookEntity book);
    }
}

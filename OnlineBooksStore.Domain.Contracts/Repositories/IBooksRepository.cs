using OnlineBooksStore.Domain.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Pages;

namespace OnlineBooksStore.Domain.Contracts.Repositories
{
    public interface IBooksRepository
    {
        IEnumerable<Book> Books { get; }
        PagedList<Book> GetBooks(QueryOptions options);
        Book GetBook(long key);
        void AddBook(Book book);
        void UpdateBook(Book book);
        void Delete(Book book);
    }
}

using OnlineBooksStore.Domain.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineBooksStore.Domain.Contracts.Repositories
{
    public interface IRepository
    {
        IEnumerable<Book> Books { get; }
        Book GetBook(long key);
        void AddBook(Book book);
        void UpdateBook(Book book);
        void UpdateAll(Book[] books);
        void Delete(Book book);
    }
}

﻿using OnlineBooksStore.Domain.Contracts.Entities;

namespace OnlineBooksStore.Domain.Contracts.Repositories
{
    public interface IWebServiceRepository
    {
        object GetBook(long id);
        object GetBooks(int skip, int take);
        long StoreBook(Book book);
        void UpdateBook(Book book);
        void DeleteBook(long id);
    }
}
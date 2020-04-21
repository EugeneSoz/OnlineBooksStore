using System;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.App.Handlers.Interfaces;
using OnlineBooksStore.App.Handlers.Mapping;
using OnlineBooksStore.Domain.Contracts.Models.Books;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Domain.Contracts.Repositories;

namespace OnlineBooksStore.App.Handlers.Query
{
    public class BookQueryHandler : IQueryHandler<PageFilterQuery, PagedResponse<BookResponse>>,
        IQueryHandler<BookIdQuery, BookResponse>
    {
        private readonly IBooksRepository _booksRepository;

        public BookQueryHandler(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository ?? throw new ArgumentNullException(nameof(booksRepository));
        }

        public PagedResponse<BookResponse> Handle(PageFilterQuery query)
        {
            var options = query.MapQueryOptions();
            var bookEntities = _booksRepository.GetBooks(options);
            var booksPagedList = bookEntities.MapBookResponsePagedList();
            var result = booksPagedList.MapPagedResponse();

            return result;
        }

        public BookResponse Handle(BookIdQuery query)
        {
            var bookEntity = _booksRepository.GetBook(query.Id);
            var book = bookEntity.MapBookResponse();

            return book;
        }
    }
}
using System;
using OnlineBooksStore.App.Contracts.Command;
using OnlineBooksStore.App.Handlers.Interfaces;
using OnlineBooksStore.App.Handlers.Mapping;
using OnlineBooksStore.Domain.Contracts.Repositories;
using OnlineBooksStore.Persistence.Entities;

namespace OnlineBooksStore.App.Handlers.Command
{
    public class BookCommandHandler : ICommandHandler<CreateBookCommand, BookEntity>,
        ICommandHandler<UpdateBookCommand, bool>,
        ICommandHandler<DeleteBookCommand, bool>
    {
        private readonly IBooksRepository _booksRepository;

        public BookCommandHandler(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository ?? throw new ArgumentNullException(nameof(booksRepository));
        }

        public BookEntity Handle(CreateBookCommand command)
        {
            var book = command.MapBookEntity();
            return _booksRepository.AddBook(book);
        }

        public bool Handle(UpdateBookCommand command)
        {
            var book = command.MapBookEntity();
            return _booksRepository.UpdateBook(book);
        }

        public bool Handle(DeleteBookCommand command)
        {
            var book = command.MapBookEntity();
            return _booksRepository.Delete(book);
        }
    }
}
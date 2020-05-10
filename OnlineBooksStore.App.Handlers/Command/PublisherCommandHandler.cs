using System;
using AutoMapper;
using OnlineBooksStore.App.Contracts.Command;
using OnlineBooksStore.App.Handlers.Interfaces;
using OnlineBooksStore.App.Handlers.Mapping;
using OnlineBooksStore.Domain.Contracts.Repositories;
using OnlineBooksStore.Persistence.Entities;

namespace OnlineBooksStore.App.Handlers.Command
{
    public class PublisherCommandHandler : ICommandHandler<CreatePublisherCommand, PublisherEntity>,
        ICommandHandler<UpdatePublisherCommand, bool>,
        ICommandHandler<DeletePublisherCommand, bool>
    {
        private readonly IPublishersRepository _repository;

        public PublisherCommandHandler(IPublishersRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public PublisherEntity Handle(CreatePublisherCommand command)
        {
            var publisher = command.MapPublisherEntity();
            return _repository.AddPublisher(publisher);
        }

        public bool Handle(UpdatePublisherCommand command)
        {
            var publisher = command.MapPublisherEntity();
            return _repository.UpdatePublisher(publisher);
        }

        public bool Handle(DeletePublisherCommand command)
        {
            var publisher = command.MapPublisherEntity();
            return _repository.DeletePublisher(publisher);
        }
    }
}
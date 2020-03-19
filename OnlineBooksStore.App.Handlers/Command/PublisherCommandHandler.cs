using System;
using AutoMapper;
using OnlineBooksStore.App.Contracts.Command;
using OnlineBooksStore.App.Handlers.Interfaces;
using OnlineBooksStore.Domain.Contracts.Entities;
using OnlineBooksStore.Domain.Contracts.Repositories;

namespace OnlineBooksStore.App.Handlers.Command
{
    public class PublisherCommandHandler : ICommandHandler<CreatePublisherCommand>,
        ICommandHandler<UpdatePublisherCommand>,
        ICommandHandler<DeletePublisherCommand>
    {

        private readonly IMapper _mapper;
        private readonly IPublishersRepository _repository;

        public PublisherCommandHandler(IMapper mapper, IPublishersRepository repository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public void Handle(CreatePublisherCommand command)
        {
            var publisher = _mapper.Map<Publisher>(command);
            _repository.AddPublisher(publisher);
        }

        public void Handle(UpdatePublisherCommand command)
        {
            var publisher = _mapper.Map<Publisher>(command);
            _repository.UpdatePublisher(publisher);
        }

        public void Handle(DeletePublisherCommand command)
        {
            var publisher = _mapper.Map<Publisher>(command);
            _repository.DeletePublisher(publisher);
        }
    }
}
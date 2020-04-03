using System;
using System.Threading.Tasks;
using AutoMapper;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.App.Handlers.Interfaces;
using OnlineBooksStore.Domain.Contracts.Entities;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Domain.Contracts.Repositories;

namespace OnlineBooksStore.App.Handlers.Query
{
    public class PublisherQueryHandler : IQueryHandler<PageFilterQuery, PagedList<Publisher>>
    {
        private readonly IMapper _mapper;
        private readonly IPublishersRepository _repository;

        public PublisherQueryHandler(IMapper mapper, IPublishersRepository repository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public PagedList<Publisher> Handle(PageFilterQuery query)
        {
            var options = _mapper.Map<QueryOptions>(query);
            var publishers = _repository.GetPublishers(options);

            return publishers;
        }
    }
}
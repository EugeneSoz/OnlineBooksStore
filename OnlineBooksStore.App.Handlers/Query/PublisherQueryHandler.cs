using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.App.Handlers.Interfaces;
using OnlineBooksStore.App.Handlers.Mapping;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Domain.Contracts.Models.Publishers;
using OnlineBooksStore.Domain.Contracts.Repositories;

namespace OnlineBooksStore.App.Handlers.Query
{
    public class PublisherQueryHandler : 
        IQueryHandler<PageFilterQuery, PagedResponse<PublisherResponse>>,
        IQueryHandler<SearchTermQuery, List<PublisherResponse>>,
        IQueryHandler<EntityIdQuery, Publisher>
    {
        private readonly IMapper _mapper;
        private readonly IPublishersRepository _repository;

        public PublisherQueryHandler(IMapper mapper, IPublishersRepository repository)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public PagedResponse<PublisherResponse> Handle(PageFilterQuery query)
        {
            var options = query.MapQueryOptions();
            var publisherEntities = _repository.GetPublishers(options);

            var publishersPagedList = publisherEntities.MapPublisherResponsePagedList();
            var result = publishersPagedList.MapPagedResponse();

            return result;
        }

        public List<PublisherResponse> Handle(SearchTermQuery query)
        {
            QueryOptions options = new QueryOptions
            {
                SearchTerm = query.Value,
                SearchPropertyNames = new[] { nameof(Publisher.Name) },
                SortPropertyName = nameof(Publisher.Name),
                PageSize = 10
            };

            var publisherEntities = _repository.GetPublishers(options);
            var publishers = publisherEntities.Entities
                .Select(e => e.MapPublisherResponse())
                .ToList();

            return publishers;
        }

        public Publisher Handle(EntityIdQuery query)
        {
            var publisherEntity = _repository.GetPublisher(query.Id);
            var publisher = publisherEntity.MapPublisher();

            return publisher;
        }
    }
}
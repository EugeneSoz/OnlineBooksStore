﻿using System.Linq;
using OnlineBooksStore.App.Contracts.Command;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Domain.Contracts.Models.Publisher;
using OnlineBooksStore.Persistence.Entities;

namespace OnlineBooksStore.App.Handlers.Mapping
{
    public static class PublisherMappings
    {
        public static PublisherResponse MapPublisherResponse(this PublisherEntity publisherEntity)
        {
            return new Publisher
            {
                Id = publisherEntity.Id,
                Name = publisherEntity.Name,
                Country = publisherEntity.Country
            };
        }

        public static Publisher MapPublisher(this PublisherEntity publisherEntity)
        {
            return new Publisher
            {
                Id = publisherEntity.Id,
                Name = publisherEntity.Name,
                Country = publisherEntity.Country,
                Books = publisherEntity.Books.Select(b => new RelatedBook
                {
                    Id = b.Id,
                    Title = b.Title,
                    Authors = b.Authors,
                    RetailPrice = b.RetailPrice,
                    PurchasePrice = b.PurchasePrice
                }).ToList()
            };
        }

        public static PublisherEntity MapPublisherEntity<TCommand>(this TCommand command) where TCommand : PublisherCommand
        {
            return new PublisherEntity
            {
                Id = command.Id,
                Name = command.Name,
                Country = command.Country
            };
        }

        public static PagedList<PublisherResponse> MapPublisherResponsePagedList(this PagedList<PublisherEntity> pagedList)
        {
            return new PagedList<PublisherResponse>()
            {
                CurrentPage = pagedList.CurrentPage,
                PageSize = pagedList.PageSize,
                TotalPages = pagedList.TotalPages,
                Entities = pagedList.Entities.Select(e => new PublisherResponse
                {
                    Id = e.Id,
                    Name = e.Name,
                    Country = e.Country
                }).ToList()
            };
        }
    }
}
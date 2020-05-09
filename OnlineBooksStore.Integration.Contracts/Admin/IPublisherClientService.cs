using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineBooksStore.App.Contracts.Command;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Domain.Contracts.Models.Publishers;
using OnlineBooksStore.Persistence.Entities;

namespace OnlineBooksStore.Integration.Contracts.Admin
{
    public interface IPublisherClientService
    {
        Task<Publisher> GetPublisherAsync(EntityIdQuery query);
        Task<PagedResponse<PublisherResponse>> GetPublishersAsync(PageFilterQuery query);
        Task<List<PublisherResponse>> GetPublishersForSelectionAsync(SearchTermQuery query);
        Task CreatePublisherAsync(CreatePublisherCommand command);
        Task UpdatePublisherAsync(UpdatePublisherCommand command);
        Task DeletePublisherAsync(DeletePublisherCommand command);
    }
}
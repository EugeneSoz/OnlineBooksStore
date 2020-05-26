using System.Collections.Generic;
using System.Threading.Tasks;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Domain.Contracts.Models.Publishers;
using OnlineBooksStore.Domain.Contracts.Services;
using OnlineBooksStore.Integration.Contracts.Admin;

namespace OnlineBooksStore.Domain.Services
{
    public class PublishersService : IPublishersService
    {
        private readonly IPublisherClientService _publisherClientService;
        public PublishersService(IPropertiesService propertiesService, IPublisherClientService publisherClientService)
        {
            Properties = propertiesService;
            _publisherClientService = publisherClientService;
        }
        public IPropertiesService Properties { get; }

        public async Task<PagedResponse<PublisherResponse>> GetPublishersAsync()
        {
            var query = new PageFilterQuery();
            return await _publisherClientService.GetPublishersAsync(query);
        }
    }
}
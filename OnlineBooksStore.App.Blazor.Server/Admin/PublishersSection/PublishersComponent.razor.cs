using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Domain.Contracts.Models.Publishers;
using OnlineBooksStore.Domain.Contracts.Services;
using OnlineBooksStore.Integration.Contracts.Admin;

namespace OnlineBooksStore.App.Blazor.Server.Admin.PublishersSection
{
    [Route(AppNavLink.Publishers)]
    public partial class PublishersComponent
    {
        private List<FilterSortingProps> _properties;
        private List<PublisherResponse> _publishers = new List<PublisherResponse>();

        [Inject] private IPropertiesService PropertiesService { get; set; }
        [Inject] private IPublisherClientService PublishersClientService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var query = new PageFilterQuery();
            var response = await PublishersClientService.GetPublishersAsync(query);

            _publishers = response.Entities;
            _properties = PropertiesService.GetPublisherFilterProps();
        }
    }
}
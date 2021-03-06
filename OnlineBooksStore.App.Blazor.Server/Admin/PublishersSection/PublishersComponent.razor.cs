﻿using System.Collections.Generic;
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
        private List<FilterSortingProps> _sortingProperties = new List<FilterSortingProps>();
        private List<FilterSortingProps> _filterProperties;
        private List<PublisherResponse> _publishers;
        private List<int> _pageNumbers;
        private Pagination _pagination;

        [Inject] private IPropertiesService PropertiesService { get; set; }
        [Inject] private IPublisherClientService PublishersClientService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var query = new PageFilterQuery();
            var response = await PublishersClientService.GetPublishersAsync(query);

            _publishers = response?.Entities ?? new List<PublisherResponse>();
            _sortingProperties = PropertiesService.GetPublisherSortingProps();
            _filterProperties = PropertiesService.GetPublisherFilterProps();
            _pageNumbers = response?.PageNumbers ?? new List<int>();
            _pagination = response?.Pagination;
        }
    }
}
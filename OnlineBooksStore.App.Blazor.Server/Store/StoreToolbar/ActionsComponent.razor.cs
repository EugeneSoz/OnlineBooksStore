using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Services;

namespace OnlineBooksStore.App.Blazor.Server.Store.StoreToolbar
{
    public partial class ActionsComponent
    {
        private List<ListItem> _gridSizeProperties;
        private List<ListItem> _sortingProperties;
        [Inject] public IPropertiesService PropertiesService { get; set; }

        protected override void OnInitialized()
        {
            _gridSizeProperties = PropertiesService.GetGridSizeProperties();
            _sortingProperties = PropertiesService.GetSortingProperties();
        }
    }
}
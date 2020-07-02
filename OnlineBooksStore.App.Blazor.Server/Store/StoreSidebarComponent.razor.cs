using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.Domain.Contracts.Models.Categories;
using OnlineBooksStore.Integration.Contracts.Store;

namespace OnlineBooksStore.App.Blazor.Server.Store
{
    public partial class StoreSidebarComponent
    {
        private List<StoreCategoryResponse> _categories = new List<StoreCategoryResponse>();

        [Inject] private IStoreClientService StoreClientService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var query = new StoreCategoryQuery();
            _categories = await StoreClientService.GetStoreCategoriesAsync(query);
        }
    }
}
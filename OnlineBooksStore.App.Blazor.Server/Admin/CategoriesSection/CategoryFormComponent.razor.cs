using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Categories;
using OnlineBooksStore.Integration.Contracts.Admin;

namespace OnlineBooksStore.App.Blazor.Server.Admin.CategoriesSection
{
    [Route(AppNavLink.CategoryForm)]
    public partial class CategoryFormComponent
    {
        [Inject] public ICategoryClientService CategoryClientService { get; set; }
        private bool _isAlertVisible;
        private Category _category = new Category();

        protected override async Task OnInitializedAsync()
        {
            var query = new CategoryIdQuery() {Id = 3};
            _category = await CategoryClientService.GetCategoryAsync(query);
        }
    }
}
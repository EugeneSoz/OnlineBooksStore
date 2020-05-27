using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Categories;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Domain.Contracts.Services;
using OnlineBooksStore.Integration.Contracts.Admin;

namespace OnlineBooksStore.App.Blazor.Server.Admin.CategoriesSection
{
    [Route(AppNavLink.Categories)]
    public partial class CategoriesComponent
    {
        private List<FilterSortingProps> _sortingProperties = new List<FilterSortingProps>();
        private List<FilterSortingProps> _filterProperties;
        private List<CategoryResponse> _categories;
        private List<int> _pageNumbers;
        private Pagination _pagination;

        [Inject] private IPropertiesService PropertiesService { get; set; }
        [Inject] private ICategoryClientService CategoriesClientService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var query = new PageFilterQuery();
            var response = await CategoriesClientService.GetCategoriesAsync(query);

            _categories = response?.Entities ?? new List<CategoryResponse>();
            _sortingProperties = PropertiesService.GetCategorySortingProps();
            _filterProperties = PropertiesService.GetCategoryFilterProps();
            _pageNumbers = response?.PageNumbers ?? new List<int>();
            _pagination = response?.Pagination;
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Books;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Domain.Contracts.Services;
using OnlineBooksStore.Integration.Contracts.Admin;

namespace OnlineBooksStore.App.Blazor.Server.Admin.BooksSection
{
    [Route(AppNavLink.Books)]
    public partial class BooksComponent
    {
        private List<FilterSortingProps> _sortingProperties = new List<FilterSortingProps>();
        private List<FilterSortingProps> _filterProperties;
        private List<BookResponse> _books;
        private List<int> _pageNumbers;
        private Pagination _pagination;

        [Inject] private IPropertiesService PropertiesService { get; set; }
        [Inject] private IBookClientService BooksClientService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var query = new PageFilterQuery();
            var response = await BooksClientService.GetBooksAsync(query);

            _books = response?.Entities ?? new List<BookResponse>();
            _sortingProperties = PropertiesService.GetBooksSortingProps();
            _filterProperties = PropertiesService.GetBookFilterProps();
            _pageNumbers = response?.PageNumbers ?? new List<int>();
            _pagination = response?.Pagination;
        }
    }
}
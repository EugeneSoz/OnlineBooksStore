using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Books;
using OnlineBooksStore.Integration.Contracts.Admin;

namespace OnlineBooksStore.App.Blazor.Server.Admin.BooksSection
{
    [Route(AppNavLink.BookForm)]
    public partial class BookFormComponent
    {
        [Inject] public IBookClientService BookClientService { get; set; }
        private bool _isAlertVisible;
        private BookResponse _book = new BookResponse();

        protected override async Task OnInitializedAsync()
        {
            var query = new BookIdQuery() {Id = 3};
            _book = await BookClientService.GetBookAsync(query);
        }
    }
}
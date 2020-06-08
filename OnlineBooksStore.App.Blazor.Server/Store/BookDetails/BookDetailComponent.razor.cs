using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Books;
using OnlineBooksStore.Integration.Contracts.Admin;

namespace OnlineBooksStore.App.Blazor.Server.Store.BookDetails
{
    [Route(AppNavLink.Details)]
    public partial class BookDetailComponent
    {
        private BookResponse _book;
        [Inject] private IBookClientService BooksClientService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            _book = await BooksClientService.GetBookAsync(new BookIdQuery() {Id = 3});
        }
    }
}
using Microsoft.AspNetCore.Components;
using OnlineBooksStore.Domain.Contracts.Models.Books;

namespace OnlineBooksStore.App.Blazor.Server.Admin.BooksSection
{
    public partial class BookInfoComponent
    {
        [Parameter] public BookResponse BookResponse { get; set; }
    }
}
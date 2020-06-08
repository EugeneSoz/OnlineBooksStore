using Microsoft.AspNetCore.Components;
using OnlineBooksStore.Domain.Contracts.Models.Books;

namespace OnlineBooksStore.App.Blazor.Server.Store.BooksList
{
    public partial class BookCardComponent
    {
        [Parameter] public BookResponse Book { get; set; }
    }
}
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using OnlineBooksStore.Domain.Contracts.Models.Books;

namespace OnlineBooksStore.App.Blazor.Server.Admin.Shared
{
    public partial class RelatedBooksComponent
    {
        [Parameter] public List<RelatedBook> Books { get; set; }
    }
}
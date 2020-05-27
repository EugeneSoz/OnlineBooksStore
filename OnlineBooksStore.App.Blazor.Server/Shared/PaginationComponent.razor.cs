using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using OnlineBooksStore.Domain.Contracts.Models.Pages;

namespace OnlineBooksStore.App.Blazor.Server.Shared
{
    public partial class PaginationComponent
    {
        [Parameter] public Pagination Pagination { get; set; }
        [Parameter] public List<int> PageNumbers { get; set; }
    }
}
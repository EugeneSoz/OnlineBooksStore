using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using OnlineBooksStore.Domain.Contracts.Models;

namespace OnlineBooksStore.App.Blazor.Server.Admin.Shared
{
    public partial class AdminFilterComponent
    {
        private bool _isClearButtonVisible;
        [Parameter] public List<FilterSortingProps> Properties { get; set; }
        [Parameter] public string Term { get; set; } = string.Empty;

    }
}
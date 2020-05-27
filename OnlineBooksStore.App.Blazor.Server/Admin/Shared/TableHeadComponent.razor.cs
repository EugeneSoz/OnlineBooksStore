using Microsoft.AspNetCore.Components;
using OnlineBooksStore.Domain.Contracts.Models;

namespace OnlineBooksStore.App.Blazor.Server.Admin.Shared
{
    public partial class TableHeadComponent
    {
        private bool _isActive;
        private bool _descendingOrder;

        [Parameter] public FilterSortingProps Property { get; set; }
    }
}
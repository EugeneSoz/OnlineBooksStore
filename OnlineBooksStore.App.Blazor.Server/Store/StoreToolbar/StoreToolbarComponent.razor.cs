using Microsoft.AspNetCore.Components;

namespace OnlineBooksStore.App.Blazor.Server.Store.StoreToolbar
{
    public partial class StoreToolbarComponent
    {
        [Parameter] public bool IsToolbarActionsVisible { get; set; }
        [Parameter] public bool IsSearchToolbarVisible { get; set; }
    }
}
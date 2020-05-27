using Microsoft.AspNetCore.Components;

namespace OnlineBooksStore.App.Blazor.Server.Admin.Shared
{
    public partial class AdminToolbarComponent
    {
        [Parameter] public bool IsButtonsVisible { get; set; }
        [Parameter] public string Link { get; set; }
    }
}
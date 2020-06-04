using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using OnlineBooksStore.Domain.Contracts.Models;

namespace OnlineBooksStore.App.Blazor.Server.Admin
{
    [Route(AppNavLink.Home)]
    public partial class AdminHomeComponent
    {
        private bool _infoMessageVisible;
    }
}
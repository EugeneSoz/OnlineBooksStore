using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace OnlineBooksStore.App.Blazor.Server.Shared
{
    public partial class ServerValidationComponent
    {
        [Parameter] public List<string> Errors { get; set; } = new List<string> {"Server error"};
    }
}
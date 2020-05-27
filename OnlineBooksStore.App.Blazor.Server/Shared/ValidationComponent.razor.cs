using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace OnlineBooksStore.App.Blazor.Server.Shared
{
    public partial class ValidationComponent
    {
        [Parameter] public List<string> Messages { get; set; } = new List<string> { "Enter the value" };
    }
}
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Publishers;
using OnlineBooksStore.Integration.Contracts.Admin;

namespace OnlineBooksStore.App.Blazor.Server.Admin.PublishersSection
{
    [Route(AppNavLink.PublisherForm)]
    public partial class PablisherFormComponent
    {
        [Inject] public IPublisherClientService PublisherClientService { get; set; }
        private bool _isAlertVisible;
        private Publisher _publisher = new Publisher();

        protected override async Task OnInitializedAsync()
        {
            var query = new PublisherIdQuery {Id = 3};
            _publisher = await PublisherClientService.GetPublisherAsync(query);
        }
    }
}
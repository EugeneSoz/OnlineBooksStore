using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using OnlineBooksStore.App.Contracts.Command;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Domain.Contracts.Models.Publishers;
using OnlineBooksStore.Integration.Contracts.Admin;

namespace OnlineBooksStore.Integration.Client.Admin
{
    public class PublisherClientService : RestService, IPublisherClientService
    {
        private const string Controller = "api/Publisher";
        public PublisherClientService(HttpClient httpClient) : base(httpClient) { }

        public async Task<Publisher> GetPublisherAsync(EntityIdQuery query)
        {
            return await GetJsonAsync<Publisher>($"{Controller}/{query.Id}");
        }

        public async Task<PagedResponse<PublisherResponse>> GetPublishersAsync(PageFilterQuery query)
        {
            return await PostJsonAsync<PagedResponse<PublisherResponse>>($"{Controller}/publishers", query);
        }

        public async Task<List<PublisherResponse>> GetPublishersForSelectionAsync(SearchTermQuery query)
        {
            return await PostJsonAsync<List<PublisherResponse>>($"{Controller}/publishersforselection", query);
        }

        public async Task CreatePublisherAsync(CreatePublisherCommand command)
        {
            await PostJsonAsync<bool>($"{Controller}/create", command);
        }

        public async Task UpdatePublisherAsync(UpdatePublisherCommand command)
        {
            await PostJsonAsync<bool>($"{Controller}/update", command);
        }

        public async Task DeletePublisherAsync(DeletePublisherCommand command)
        {
            await PostJsonAsync<bool>($"{Controller}/delete", command);
        }
    }
}
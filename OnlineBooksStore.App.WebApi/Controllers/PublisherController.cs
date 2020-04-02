using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBooksStore.App.WebApi.Data;
using OnlineBooksStore.App.WebApi.Data.DTO;
using OnlineBooksStore.App.WebApi.Infrastructure;
using OnlineBooksStore.App.WebApi.Models;
using OnlineBooksStore.App.WebApi.Models.Repo;

namespace OnlineBooksStore.App.WebApi.Controllers
{
    [Authorize(Roles = "Administrator")]
    [ValidateAntiForgeryToken]
    public class PublisherController : BaseController
    {
        private readonly IPublisherRepo _repo;

        public PublisherController(IPublisherRepo repo) => _repo = repo;

        [HttpGet("publisher/{id}")]
        public async Task<Publisher> GetPublisherAsync(long id)
        {
            return await _repo.GetPublisherAsync(id);
        }

        [HttpPost("publishers")]
        public async Task<PagedResponse<Publisher>> GetPublishersAsync([FromBody] QueryOptions options)
        {
            PagedList<Publisher> publishers = await _repo.GetPublishersAsync(options);

            return publishers?.MapPagedResponse();
        }

        [HttpPost("publishersforselection")]
        public async Task<List<Publisher>> GetPublishersForSelectionAsync([FromBody] SearchTerm term)
        {
            QueryOptions options = new QueryOptions
            {
                SearchTerm = term.Value,
                SearchPropertyNames = new string[] { nameof(Publisher.Name) },
                SortPropertyName = nameof(Publisher.Name),
                PageSize = 10
            };

            PagedList<Publisher> pagedPublishers = await _repo.GetPublishersAsync(options);

            return pagedPublishers.Entities;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreatePublisherAsync([FromBody] PublisherDTO publisherDTO)
        {
            Publisher publisher = publisherDTO.MapPublisher();
            return await CreateAsync(publisher, _repo.AddAsync);
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdatePublisherAsync([FromBody] Publisher publisherDTO)
        {
            Publisher publisher = publisherDTO.MapPublisher();
            return await UpdateAsync(publisher, _repo.UpdateAsync);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> DeletePublisherAsync([FromBody] Publisher publisherDTO)
        {
            Publisher publisher = publisherDTO.MapPublisher();
            return await DeleteAsync(publisher, _repo.DeleteAsync);
        }
    }
}

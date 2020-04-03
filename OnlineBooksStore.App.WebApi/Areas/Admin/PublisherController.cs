using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineBooksStore.App.WebApi.Data;
using OnlineBooksStore.App.WebApi.Data.DTO;
using OnlineBooksStore.App.WebApi.Infrastructure;
using OnlineBooksStore.App.WebApi.Models;
using OnlineBooksStore.App.WebApi.Models.Repo;

namespace OnlineBooksStore.App.WebApi.Areas.Admin
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ValidateAntiForgeryToken]
    public class PublisherController : Controller
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
                SearchPropertyNames = new[] { nameof(Publisher.Name) },
                SortPropertyName = nameof(Publisher.Name),
                PageSize = 10
            };

            PagedList<Publisher> pagedPublishers = await _repo.GetPublishersAsync(options);

            return pagedPublishers.Entities;
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreatePublisherAsync([FromBody] PublisherDTO publisherDTO)
        {
            if (!ModelState.IsValid)
            {
                return Ok(GetServerErrors(ModelState));
            }

            Publisher publisher;
            try
            {
                publisher = publisherDTO.MapPublisher();
                await _repo.AddAsync(publisher);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $@"Невозможно создать запись: {ex.Message}");
                return BadRequest(GetServerErrors(ModelState));
            }

            return Created("", publisher);
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdatePublisherAsync([FromBody] Publisher publisherDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetServerErrors(ModelState));
            }

            bool isOk;
            try
            {
                Publisher publisher = publisherDTO.MapPublisher();
                isOk = await _repo.UpdateAsync(publisher);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $@"Невозможно сохранить запись: {ex.Message}");
                return BadRequest(GetServerErrors(ModelState));
            }

            if (!isOk)
            {
                return NotFound();
            }
            return Ok();
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> DeletePublisherAsync([FromBody] Publisher publisherDTO)
        {
            bool isOk;

            try
            {
                Publisher publisher = publisherDTO.MapPublisher();
                isOk = await _repo.DeleteAsync(publisher);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $@"Невозможно удалить запись: {ex.Message}");
                return BadRequest(GetServerErrors(ModelState));
            }

            if (!isOk)
            {
                return NotFound();
            }
            return NoContent();
        }

        private List<string> GetServerErrors(ModelStateDictionary modelstate)
        {
            List<string> errors = new List<string>();
            foreach (ModelStateEntry error in modelstate.Values)
            {
                foreach (ModelError e in error.Errors)
                {
                    errors.Add(e.ErrorMessage);
                }
            }

            return errors;
        }
    }
}

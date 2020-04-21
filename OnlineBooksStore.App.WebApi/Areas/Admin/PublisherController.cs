using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using OnlineBooksStore.App.Contracts.Command;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.App.Handlers.Command;
using OnlineBooksStore.App.Handlers.Query;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Domain.Contracts.Models.Publishers;
using OnlineBooksStore.Persistence.Entities;

namespace OnlineBooksStore.App.WebApi.Areas.Admin
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ValidateAntiForgeryToken]
    public class PublisherController : Controller
    {
        private readonly PublisherCommandHandler _commandHandler;
        private readonly PublisherQueryHandler _queryHandler;

        public PublisherController(PublisherCommandHandler commandHandler, PublisherQueryHandler queryHandler)
        {
            _commandHandler = commandHandler;
            _queryHandler = queryHandler;
        }

        [HttpGet("publisher/{id}")]
        public Publisher GetPublisher([FromRoute] EntityIdQuery query)
        {
            return _queryHandler.Handle(query);
        }

        [HttpPost("publishers")]
        public PagedResponse<PublisherResponse> GetPublishers([FromBody] PageFilterQuery query)
        {
            return _queryHandler.Handle(query);
        }

        [HttpPost("publishersforselection")]
        public List<PublisherResponse> GetPublishersForSelection([FromBody] SearchTermQuery query)
        {
            return _queryHandler.Handle(query);
        }

        [HttpPost("create")]
        public ActionResult CreatePublisher([FromBody] CreatePublisherCommand command)
        {
            if (!ModelState.IsValid)
            {
                return Ok(GetServerErrors(ModelState));
            }

            PublisherEntity publisher;
            try
            {
                publisher = _commandHandler.Handle(command);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $@"Невозможно создать запись: {ex.Message}");
                return BadRequest(GetServerErrors(ModelState));
            }

            return Created("", publisher);
        }

        [HttpPut("update")]
        public ActionResult UpdatePublisher([FromBody] UpdatePublisherCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetServerErrors(ModelState));
            }

            bool isOk;
            try
            {
                isOk = _commandHandler.Handle(command);
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
        public ActionResult DeletePublisher([FromBody] DeletePublisherCommand command)
        {
            bool isOk;

            try
            {
                isOk = _commandHandler.Handle(command);
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

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
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Pages;

namespace OnlineBooksStore.App.WebApi.Areas.Admin
{
    [Authorize(Roles = "Administrator")]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [AutoValidateAntiforgeryToken]
    public class BookController : Controller
    {
        private readonly IBookRepo _repo;

        public BookController(IBookRepo repo) => _repo = repo;

        [HttpGet("book/{id}")]
        public async Task<BookResponse> GetBookAsync(long id)
        {
            return await _repo.GetBookAsync(id);
        }

        [HttpPost("books")]
        public async Task<PagedResponse<BookResponse>> GetBooksAsync([FromBody] QueryOptions options)
        {
            PagedList<BookResponse> books = await _repo.GetBooksAsync(options);

            return books.MapPagedResponse();
        }

        [HttpPost("create")]
        public async Task<ActionResult> CreateBookAsync([FromBody] BookDTO bookDTO)
        {
            if (!ModelState.IsValid)
            {
                return Ok(GetServerErrors(ModelState));
            }

            Book book;
            try
            {
                book = bookDTO.MapBook();
                await _repo.AddAsync(book);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $@"Невозможно создать запись: {ex.Message}");
                return BadRequest(GetServerErrors(ModelState));
            }

            return Created("", book);
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateBookAsync([FromBody] BookDTO bookDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(GetServerErrors(ModelState));
            }

            bool isOk;
            try
            {
                Book book = bookDTO.MapBook();
                isOk = await _repo.UpdateAsync(book);
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
        public async Task<ActionResult> DeleteTaskAsync([FromBody] BookDTO bookDTO)
        {
            bool isOk;

            try
            {
                Book book = bookDTO.MapBook();
                isOk = await _repo.DeleteAsync(book);
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

using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineBooksStore.App.WebApi.Controllers;
using OnlineBooksStore.App.WebApi.Data;
using OnlineBooksStore.App.WebApi.Data.DTO;
using OnlineBooksStore.App.WebApi.Infrastructure;
using OnlineBooksStore.App.WebApi.Models;
using OnlineBooksStore.App.WebApi.Models.Repo;

namespace OnlineBooksStore.App.WebApi.Areas.Admin
{
    [Authorize(Roles = "Administrator")]
    [AutoValidateAntiforgeryToken]
    public class BookController : BaseController
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
            Book book = bookDTO.MapBook();
            return await CreateAsync(book, _repo.AddAsync);
        }

        [HttpPut("update")]
        public async Task<ActionResult> UpdateBookAsync([FromBody] BookDTO bookDTO)
        {
            Book book = bookDTO.MapBook();
            return await UpdateAsync(book, _repo.UpdateAsync);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteTaskAsync([FromBody] BookDTO bookDTO)
        {
            Book book = bookDTO.MapBook();
            return await DeleteAsync(book, _repo.DeleteAsync);
        }
    }
}

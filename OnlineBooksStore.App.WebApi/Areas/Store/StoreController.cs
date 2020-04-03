using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineBooksStore.App.WebApi.Data.DTO;
using OnlineBooksStore.App.WebApi.Infrastructure;
using OnlineBooksStore.App.WebApi.Models;
using OnlineBooksStore.App.WebApi.Models.Repo;

namespace OnlineBooksStore.App.WebApi.Areas.Store
{
    [Route("api/[controller]")]
    public class StoreController : Controller
    {
        private readonly IBookRepo _bookRepo;
        private readonly ICategoryRepo _categoryRepo;

        public StoreController(IBookRepo bookRepo, ICategoryRepo categoryRepo)
        {
            _bookRepo = bookRepo;
            _categoryRepo = categoryRepo;
        }
        [HttpGet("book/{id}")]
        public async Task<BookResponse> GetBookAsync(long id)
        {
            return await _bookRepo.GetBookAsync(id);
        }

        [HttpPost("books")]
        public async Task<PagedResponse<BookResponse>> GetBooksAsync([FromBody] QueryOptions options)
        {
            PagedList<BookResponse> books = await _bookRepo.GetBooksAsync(options);

            return books.MapPagedResponse();
        }

        [HttpGet("categories")]
        public async Task<List<StoreCategoryResponse>> GetStoreCategoriesAsync()
        {
            return await _categoryRepo.GetStoreCategoriesAsync();
        }
    }
}

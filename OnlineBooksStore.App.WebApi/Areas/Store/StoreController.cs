using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.App.Handlers.Query;
using OnlineBooksStore.App.WebApi.Infrastructure;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Category;
using OnlineBooksStore.Domain.Contracts.Models.Pages;

namespace OnlineBooksStore.App.WebApi.Areas.Store
{
    [Route("api/[controller]")]
    public class StoreController : Controller
    {
        private readonly IBookRepo _bookRepo;
        private readonly CategoryQueryHandler _queryHandler;

        public StoreController(IBookRepo bookRepo, CategoryQueryHandler queryHandler)
        {
            _bookRepo = bookRepo;
            _queryHandler = queryHandler;
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
        public List<StoreCategoryResponse> GetStoreCategories([FromQuery] StoreCategoryQuery query)
        {
            return _queryHandler.Handle(query);
        }
    }
}

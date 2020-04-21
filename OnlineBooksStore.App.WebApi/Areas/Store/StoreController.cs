using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.App.Handlers.Query;
using OnlineBooksStore.Domain.Contracts.Models.Books;
using OnlineBooksStore.Domain.Contracts.Models.Categories;
using OnlineBooksStore.Domain.Contracts.Models.Pages;

namespace OnlineBooksStore.App.WebApi.Areas.Store
{
    [Route("api/[controller]")]
    public class StoreController : Controller
    {
        private readonly BookQueryHandler _bookQueryHandler;
        private readonly CategoryQueryHandler _categoryQueryHandler;

        public StoreController(BookQueryHandler bookQueryHandler, CategoryQueryHandler categoryQueryHandler)
        {
            _bookQueryHandler = bookQueryHandler;
            _categoryQueryHandler = categoryQueryHandler;
        }

        [HttpGet("book/{id}")]
        public BookResponse GetBook([FromQuery] BookIdQuery query)
        {
            return _bookQueryHandler.Handle(query);
        }

        [HttpPost("books")]
        public PagedResponse<BookResponse> GetBooks([FromBody] PageFilterQuery query)
        {
            return _bookQueryHandler.Handle(query);
        }

        [HttpGet("categories")]
        public List<StoreCategoryResponse> GetStoreCategories([FromQuery] StoreCategoryQuery query)
        {
            return _categoryQueryHandler.Handle(query);
        }
    }
}

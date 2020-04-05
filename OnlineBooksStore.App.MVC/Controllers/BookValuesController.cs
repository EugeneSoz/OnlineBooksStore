using System;
using Microsoft.AspNetCore.Mvc;
using OnlineBooksStore.Domain.Contracts.Entities;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Books;
using OnlineBooksStore.Domain.Contracts.Repositories;

namespace OnlineBooksStore.App.MVC.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/books")]
    public class BookValuesController : ControllerBase
    {
        private readonly IWebServiceRepository _webServiceRepository;

        public BookValuesController(IWebServiceRepository webServiceRepository)
        {
            _webServiceRepository = webServiceRepository ?? throw new ArgumentNullException(nameof(webServiceRepository));
        }

        [HttpGet("{id}")]
        public object GetBook(long id)
        {
            return _webServiceRepository.GetBook(id) ?? NotFound();
        }

        [HttpGet]
        public object Books(int skip, int take)
        {
            return _webServiceRepository.GetBooks(skip, take);
        }

        [HttpPost]
        public long StoreBook([FromBody] Book book)
        {
            return _webServiceRepository.StoreBook(book);
        }

        [HttpPut]
        public void UpdateBook([FromBody] Book book)
        {
            _webServiceRepository.UpdateBook(book);
        }

        [HttpDelete("{id}")]
        public void DeleteBook(long id)
        {
            _webServiceRepository.DeleteBook(id);
        }
    }
}
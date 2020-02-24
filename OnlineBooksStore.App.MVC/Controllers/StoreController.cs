using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Domain.Contracts.Repositories;

namespace OnlineBooksStore.App.MVC.Controllers
{
    public class StoreController : Controller
    {
        private readonly ICategoriesRepository _categoryRepository;
        private readonly IBooksRepository _bookRepository;

        public StoreController(ICategoriesRepository categoryRepository, IBooksRepository bookRepository)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
        }

        public IActionResult Index([FromQuery(Name = "options")] QueryOptions productOptions, QueryOptions catOptions, long category)
        {
            ViewBag.Categories = _categoryRepository.GetCategories(catOptions);
            ViewBag.SelectedCategory = category;

            return View(_bookRepository.GetBooks(productOptions, category));
        }
    }
}
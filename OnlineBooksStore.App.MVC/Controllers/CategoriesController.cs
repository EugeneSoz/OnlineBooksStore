using System;
using Microsoft.AspNetCore.Mvc;
using OnlineBooksStore.Domain.Contracts.Entities;
using OnlineBooksStore.Domain.Contracts.Models;
using OnlineBooksStore.Domain.Contracts.Models.Categories;
using OnlineBooksStore.Domain.Contracts.Models.Pages;
using OnlineBooksStore.Domain.Contracts.Repositories;

namespace OnlineBooksStore.App.MVC.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesController(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository ?? throw new ArgumentNullException(nameof(categoriesRepository));
        }

        public IActionResult Index(QueryOptions options) => View(_categoriesRepository.GetCategories(options));

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            _categoriesRepository.AddCategory(category);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditCategory(long id)
        {
            ViewBag.EditId = id;
            return View("Index", _categoriesRepository.Categories);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category category)
        {
            _categoriesRepository.UpdateCategory(category);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeleteCategory(Category category)
        {
            _categoriesRepository.DeleteCategory(category);
            return RedirectToAction(nameof(Index));
        }
    }
}
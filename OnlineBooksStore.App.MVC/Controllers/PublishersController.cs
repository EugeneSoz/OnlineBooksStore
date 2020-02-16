using System;
using Microsoft.AspNetCore.Mvc;
using OnlineBooksStore.Domain.Contracts.Entities;
using OnlineBooksStore.Domain.Contracts.Repositories;

namespace OnlineBooksStore.App.MVC.Controllers
{
    public class PublishersController : Controller
    {
        private readonly IPublishersRepository _publishersRepository;

        public PublishersController(IPublishersRepository publishersRepository)
        {
            _publishersRepository = publishersRepository ?? throw new ArgumentNullException(nameof(publishersRepository));
        }

        public IActionResult Index() => View(_publishersRepository.Publishers);

        [HttpPost]
        public IActionResult AddPublisher(Publisher publisher)
        {
            _publishersRepository.AddPublisher(publisher);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditPublisher(long id)
        {
            ViewBag.EditId = id;
            return View("Index", _publishersRepository.Publishers);
        }

        [HttpPost]
        public IActionResult UpdatePublisher(Publisher publisher)
        {
            _publishersRepository.UpdatePublisher(publisher);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeletePublisher(Publisher publisher)
        {
            _publishersRepository.DeletePublisher(publisher);

            return RedirectToAction(nameof(Index));
        }
    }
}
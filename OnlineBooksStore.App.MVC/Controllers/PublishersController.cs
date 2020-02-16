using System;
using Microsoft.AspNetCore.Mvc;
using OnlineBooksStore.Domain.Contracts.Entities;
using OnlineBooksStore.Domain.Contracts.Repositories;

namespace OnlineBooksStore.App.MVC.Controllers
{
    public class PublishersController : Controller
    {
        private readonly IPublisherRepository _publisherRepository;

        public PublishersController(IPublisherRepository publisherRepository)
        {
            _publisherRepository = publisherRepository ?? throw new ArgumentNullException(nameof(publisherRepository));
        }

        public IActionResult Index() => View(_publisherRepository.Publishers);

        [HttpPost]
        public IActionResult AddPublisher(Publisher publisher)
        {
            _publisherRepository.AddPublisher(publisher);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditPublisher(long id)
        {
            ViewBag.EditId = id;
            return View("Index", _publisherRepository.Publishers);
        }

        [HttpPost]
        public IActionResult UpdatePublisher(Publisher publisher)
        {
            _publisherRepository.UpdatePublisher(publisher);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeletePublisher(Publisher publisher)
        {
            _publisherRepository.DeletePublisher(publisher);

            return RedirectToAction(nameof(Index));
        }
    }
}
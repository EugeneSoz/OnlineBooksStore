using System;
using Microsoft.AspNetCore.Mvc;
using OnlineBooksStore.App.Contracts.Command;
using OnlineBooksStore.App.Contracts.Query;
using OnlineBooksStore.App.Handlers.Command;
using OnlineBooksStore.App.Handlers.Query;
using OnlineBooksStore.Domain.Contracts.Repositories;

namespace OnlineBooksStore.App.MVC.Controllers
{
    public class PublishersController : Controller
    {
        private readonly IPublishersRepository _publishersRepository;
        private readonly PublisherQueryHandler _queryHandler;
        private readonly PublisherCommandHandler _commandHandler;

        public PublishersController(
            IPublishersRepository publishersRepository,
            PublisherQueryHandler queryHandler,
            PublisherCommandHandler commandHandler)
        {
            _publishersRepository = publishersRepository ?? throw new ArgumentNullException(nameof(publishersRepository));
            _queryHandler = queryHandler ?? throw new ArgumentNullException(nameof(queryHandler));
            _commandHandler = commandHandler ?? throw new ArgumentNullException(nameof(commandHandler));
        }

        public IActionResult Index(PageFilterQuery query) => View(_queryHandler.Handle(query));

        [HttpPost]
        public IActionResult AddPublisher(CreatePublisherCommand command)
        {
            _commandHandler.Handle(command);

            return RedirectToAction(nameof(Index));
        }

        public IActionResult EditPublisher(PublisherIdQuery query)
        {
            ViewBag.EditId = query.Id;
            return View("Index", _publishersRepository.Publishers);
        }

        [HttpPost]
        public IActionResult UpdatePublisher(UpdatePublisherCommand command)
        {
            _commandHandler.Handle(command);

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult DeletePublisher(DeletePublisherCommand command)
        {
            _commandHandler.Handle(command);

            return RedirectToAction(nameof(Index));
        }
    }
}
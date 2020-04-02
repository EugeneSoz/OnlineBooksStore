using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OnlineBooksStore.App.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PublishersController : Controller
    {
        public IActionResult ShowPublishers()
        {
            return View();
        }

        public IActionResult CreateOrEditPublisher()
        {
            return View();
        }
    }
}
using Microsoft.AspNetCore.Mvc;

namespace OnlineBooksStore.App.MVC.Areas.Store.Controllers
{
    [Area("Store")]
    public class HomeController : Controller
    {
        public IActionResult ShowBooks()
        {
            return View();
        }

        public IActionResult GetBookDetails()
        {
            return View();
        }
    }
}
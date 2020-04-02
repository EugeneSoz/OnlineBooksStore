using Microsoft.AspNetCore.Mvc;

namespace OnlineBooksStore.App.MVC.Areas.Orders.Controllers
{
    [Area("Orders")]
    public class OrdersController : Controller
    {
        public IActionResult GetCartDetails()
        {
            return View();
        }

        public IActionResult Checkout()
        {
            return View();
        }

        public IActionResult ShowOrderInfo()
        {
            return View();
        }
    }
}
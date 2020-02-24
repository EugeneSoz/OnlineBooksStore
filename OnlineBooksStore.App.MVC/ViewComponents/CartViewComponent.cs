using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using OnlineBooksStore.App.MVC.Infrastructure;
using OnlineBooksStore.Domain.Contracts.Models;

namespace OnlineBooksStore.App.MVC.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.GetJson<Cart>("Cart");
            return View(cart);
        }
    }
}
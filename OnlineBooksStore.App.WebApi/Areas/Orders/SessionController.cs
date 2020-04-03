using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OnlineBooksStore.App.WebApi.Data.DTO;

namespace OnlineBooksStore.App.WebApi.Areas.Orders
{
    [Route("api/session")]
    [AutoValidateAntiforgeryToken]
    public class SessionController : ControllerBase
    {
        [HttpGet("cart")]
        public ActionResult GetCart()
        {
            string result = HttpContext.Session.GetString("cart");

            return Ok(result);
        }

        [HttpPost("cart")]
        public ActionResult StoreCart([FromBody] CartLine[] products)
        {
            string jsonData = JsonConvert.SerializeObject(products, 
                new JsonSerializerSettings {
                    ContractResolver = new CamelCasePropertyNamesContractResolver() });
            HttpContext.Session.SetString("cart", jsonData);

            return Ok(null);
        }

        [HttpGet("checkout")]
        public IActionResult GetCheckout()
        {
            return Ok(HttpContext.Session.GetString("checkout"));
        }

        [HttpPost("checkout")]
        public void StoreCheckout([FromBody] CheckoutState data)
        {
            string jsonData = JsonConvert.SerializeObject(data, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            HttpContext.Session.SetString("checkout", jsonData);
        }
    }
}

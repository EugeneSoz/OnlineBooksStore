using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineBooksStore.App.WebApi.Models;

namespace OnlineBooksStore.App.WebApi.Areas.Store
{
    [Area("Store")]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private UserManager<IdentityUser> userManager;
        private SignInManager<IdentityUser> signInManager;
        public AccountController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr)
        {
            userManager = userMgr;
            signInManager = signInMgr;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login([FromBody] Login creds)
        {
            if (ModelState.IsValid && await DoLogin(creds))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await signInManager.SignOutAsync();

            return Ok();
        }

        private async Task<bool> DoLogin(Login creds)
        {
            IdentityUser user = await userManager.FindByNameAsync(creds.Name);
            if (user != null)
            {
                await signInManager.SignOutAsync();
                Microsoft.AspNetCore.Identity.SignInResult result =
                    await signInManager.PasswordSignInAsync(user, creds.Password, false, false);

                return result.Succeeded;
            }
            return false;
        }
    }
}

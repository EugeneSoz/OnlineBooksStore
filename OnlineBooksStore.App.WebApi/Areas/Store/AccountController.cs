using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OnlineBooksStore.App.WebApi.Models;
using OnlineBooksStore.Domain.Contracts.Models;

namespace OnlineBooksStore.App.WebApi.Areas.Store
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(UserManager<IdentityUser> userMgr, SignInManager<IdentityUser> signInMgr)
        {
            _userManager = userMgr;
            _signInManager = signInMgr;
        }

        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync([FromBody] Login creds)
        {
            if (ModelState.IsValid && await LoginUsingCredentialsAsync(creds))
            {
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("logout")]
        public async Task<ActionResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();

            return Ok();
        }

        private async Task<bool> LoginUsingCredentialsAsync(Login creds)
        {
            IdentityUser user = await _userManager.FindByNameAsync(creds.Name);
            if (user != null)
            {
                await _signInManager.SignOutAsync();
                Microsoft.AspNetCore.Identity.SignInResult result =
                    await _signInManager.PasswordSignInAsync(user, creds.Password, false, false);

                return result.Succeeded;
            }
            return false;
        }
    }
}

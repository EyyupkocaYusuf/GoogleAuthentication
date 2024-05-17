using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;

namespace GoogleAuthentication.Controllers
{
	public class LoginController : Controller
	{
		public IActionResult Index()
		{
			//selam
			return View();
		}

		public async Task Login()
		{
			await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
				new AuthenticationProperties
				{
					RedirectUri = Url.Action("GoogleResponse")
				});
		}

		public async Task<IActionResult> GoogleResponse()
		{
			var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
			{
				claim.Issuer,
				claim.OriginalIssuer,
				claim.Type,
				claim.Value
			}); // kimlik bilgilerini alır.

			// return Json(claims);

			return RedirectToAction("Index", "Home", new { area = "" });
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			return View("Index");
		}
	}
}

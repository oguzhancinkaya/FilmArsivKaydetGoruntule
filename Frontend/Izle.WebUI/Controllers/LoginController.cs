using Izle.EntityLayer.Concrete;
using Izle.WebUI.Dtos.LoginDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Izle.WebUI.Controllers
{
	public class LoginController : Controller
	{
		private readonly SignInManager<AppUser> _signInManager;

		public LoginController(SignInManager<AppUser> signInManager)
		{
			_signInManager = signInManager;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task <IActionResult> Index(LoginUserDto loginUserDto)
		{
			if(ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(loginUserDto.UserName, loginUserDto.Password,false,false);
				if(result.Succeeded)
				{
					return RedirectToAction("Index", "Movie");
				}
				else
				{
					return View();
				}
			}
			return View();
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> LogOut()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Movie");
		}



	}
}

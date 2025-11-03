using Izle.EntityLayer.Concrete;
using Izle.WebUI.Dtos.RegisterDto;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Izle.WebUI.Controllers
{
	public class RegisterController : Controller
	{
		private readonly UserManager<AppUser> _userManager;

		public RegisterController(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		[HttpGet]
		public IActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Index(CreateNewUserDto createNewUserDto)
		{
			if(!ModelState.IsValid)
			{
				return View();
			}
			var appUser = new AppUser()
			{
				NameSurname = createNewUserDto.NameSurname,
				Email = createNewUserDto.Mail,
				UserName = createNewUserDto.UserName,
				UserImage = createNewUserDto.UserImage
			};
			var result = await _userManager.CreateAsync(appUser,
				createNewUserDto.Password);
			if(result.Succeeded)
			{
				return RedirectToAction("Index", "Login");
			}
			return View();
		}
	}
}

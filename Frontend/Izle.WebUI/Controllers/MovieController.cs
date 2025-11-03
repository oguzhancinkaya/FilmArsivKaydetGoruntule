using Izle.WebUI.Models.Movie;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Izle.WebUI.Controllers
{
	public class MovieController : Controller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public MovieController(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<IActionResult> Index()
		{
			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync("http://localhost:5078/api/Movie");
			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var values = JsonConvert.DeserializeObject<List<Movie>>(jsonData);
				return View(values);
			}
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> MovieDetail(int id)
		{
			if (id == 0)
				return NotFound();

			var client = _httpClientFactory.CreateClient();
			var responseMessage = await client.GetAsync($"http://localhost:5078/api/Movie/{id}");

			if (responseMessage.IsSuccessStatusCode)
			{
				var jsonData = await responseMessage.Content.ReadAsStringAsync();
				var movie = JsonConvert.DeserializeObject<Movie>(jsonData);
				if (movie == null)
					return NotFound();

				return View(movie);
			}

			return NotFound();
		}
	}
}

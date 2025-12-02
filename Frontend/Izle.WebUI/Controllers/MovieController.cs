// Izle.WebUI/Controllers/MovieController.cs
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

        public async Task<IActionResult> Index(int page = 1)
        {
            int pageSize = 20; // Her sayfada 20 film

            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("http://localhost:5078/api/Movie");

            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var allMovies = JsonConvert.DeserializeObject<List<Movie>>(jsonData);

                // Toplam sayfa sayısını hesapla
                int totalMovies = allMovies.Count;
                int totalPages = (int)Math.Ceiling(totalMovies / (double)pageSize);

                // Sayfalama için filmleri al
                var pagedMovies = allMovies
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                // ViewBag ile sayfalama bilgilerini gönder
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;
                ViewBag.TotalMovies = totalMovies;

                return View(pagedMovies);
            }

            return View(new List<Movie>());
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
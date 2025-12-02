// Izle.WebUI/Controllers/UserMovieController.cs
using Izle.WebUI.Models.UserMovie;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;

namespace Izle.WebUI.Controllers
{
    [Authorize]
    public class UserMovieController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public UserMovieController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private int GetUserId()
        {
            return int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int movieId, string status)
        {
            var userId = GetUserId();
            var client = _httpClientFactory.CreateClient();

            var requestData = new
            {
                UserId = userId,
                MovieId = movieId,
                Status = status
            };

            var jsonContent = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:5078/api/UserMovie", content);

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int movieId)
        {
            var userId = GetUserId();
            var client = _httpClientFactory.CreateClient();

            var response = await client.DeleteAsync($"http://localhost:5078/api/UserMovie?userId={userId}&movieId={movieId}");

            if (response.IsSuccessStatusCode)
            {
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

        public async Task<IActionResult> Watched()
        {
            var userId = GetUserId();
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync($"http://localhost:5078/api/UserMovie/user/{userId}/status/Watched");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<UserMovie>>(jsonData);
                return View(values);
            }

            return View(new List<UserMovie>());
        }

        public async Task<IActionResult> Watching()
        {
            var userId = GetUserId();
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync($"http://localhost:5078/api/UserMovie/user/{userId}/status/Watching");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<UserMovie>>(jsonData);
                return View(values);
            }

            return View(new List<UserMovie>());
        }

        public async Task<IActionResult> WatchLater()
        {
            var userId = GetUserId();
            var client = _httpClientFactory.CreateClient();

            var response = await client.GetAsync($"http://localhost:5078/api/UserMovie/user/{userId}/status/WatchLater");

            if (response.IsSuccessStatusCode)
            {
                var jsonData = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<List<UserMovie>>(jsonData);
                return View(values);
            }

            return View(new List<UserMovie>());
        }
    }
}
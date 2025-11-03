using Izle.BusinessLayer.Abstract;
using Izle.BusinessLayer.ExternalApi;
using Izle.EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Izle.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly TmdbApiService _tmdbApiService;

        public MovieController(IMovieService movieService, TmdbApiService tmdbApiService)
        {
            _movieService = movieService;
            _tmdbApiService = tmdbApiService;
        }

        // 🔹 1. Tüm filmleri getir
        [HttpGet]
        public IActionResult MovieList()
        {
            var values = _movieService.TGetList();
            return Ok(values);
        }

        // 🔹 2. TMDB'den popüler filmleri çek ve kategorileri virgüllerle kaydet
        [HttpPost("import-popular")]
        public async Task<IActionResult> ImportPopularMovies([FromQuery] int page = 1)
        {
            // 1️⃣ Kategorileri önceden çek
            var genreList = await _tmdbApiService.GetGenresAsync();
            if (genreList == null)
                return BadRequest("TMDB tür listesi alınamadı.");

            // 2️⃣ Popüler filmleri çek
            var movies = await _tmdbApiService.GetPopularMoviesAsync(page);
            if (movies == null || !movies.Any())
                return BadRequest("TMDB'den film verisi alınamadı.");

            int addedCount = 0;

            foreach (var movieJson in movies)
            {
                // JSON verilerini çöz
                string titleTr = movieJson["title_tr"]?.ToString() ?? "";
                string titleEn = movieJson["title_en"]?.ToString() ?? "";
                string overviewTr = movieJson["overview_tr"]?.ToString() ?? "";
                string overviewEn = movieJson["overview_en"]?.ToString() ?? "";

                string finalTitle = !string.IsNullOrWhiteSpace(titleTr) ? titleTr : titleEn;
                string finalOverview = !string.IsNullOrWhiteSpace(overviewTr) ? overviewTr : overviewEn;

                // 🎬 Kategori ID'lerini al
                var genreIds = movieJson["genre_ids"]?.ToObject<List<int>>() ?? new List<int>();

                // 🎭 ID'leri kategori adına çevirip virgülle birleştir
                var genreNames = new List<string>();
                foreach (var id in genreIds)
                {
                    var matched = genreList.FirstOrDefault(g => (int)g["id"] == id);
                    if (matched != null)
                        genreNames.Add(matched["name_tr"]?.ToString() ?? matched["name_en"]?.ToString() ?? "");
                }

                string categories = string.Join(", ", genreNames.Where(x => !string.IsNullOrWhiteSpace(x)));

                // 🎥 Movie nesnesi oluştur
                var movie = new Movie
                {
                    Title = finalTitle,
                    Description = finalOverview,
                    CoverImageUrl = movieJson["poster_path"] != null
                        ? $"https://image.tmdb.org/t/p/w500{movieJson["poster_path"]}"
                        : null,
                    Rating = Convert.ToDecimal(movieJson["vote_average"] ?? 0),
                    ReleaseDate = DateTime.TryParse(movieJson["release_date"]?.ToString(), out var d)
                        ? d : DateTime.MinValue,
                    Category = categories
                };

                // Aynı isimde film varsa ekleme
                if (!_movieService.TGetList().Any(m => m.Title == movie.Title))
                {
                    _movieService.TInsert(movie);
                    addedCount++;
                }
            }

            return Ok($"{addedCount} film eklendi ve kategoriler virgülle kaydedildi.");
        }

        // 🔹 3. Film sil
        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            var movie = _movieService.TGetByID(id);
            if (movie == null) return NotFound("Film bulunamadı.");

            _movieService.TDelete(movie);
            return Ok("Film silindi.");
        }

        // 🔹 4. Film güncelle
        [HttpPut]
        public IActionResult UpdateMovie(Movie movie)
        {
            _movieService.TUpdate(movie);
            return Ok("Film güncellendi.");
        }

        // 🔹 5. ID’ye göre film getir
        [HttpGet("{id}")]
        public IActionResult GetMovie(int id)
        {
            var movie = _movieService.TGetByID(id);
            if (movie == null) return NotFound("Film bulunamadı.");
            return Ok(movie);
        }
    }
}

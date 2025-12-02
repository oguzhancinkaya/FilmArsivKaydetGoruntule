// Izle.WebApi/Controllers/UserMovieController.cs
using Izle.BusinessLayer.Abstract;
using Izle.EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Izle.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMovieController : ControllerBase
    {
        private readonly IUserMovieService _userMovieService;

        public UserMovieController(IUserMovieService userMovieService)
        {
            _userMovieService = userMovieService;
        }

        // Kullanıcının tüm film listesi
        [HttpGet("user/{userId}")]
        public IActionResult GetUserMovies(int userId)
        {
            var values = _userMovieService.TGetUserMoviesWithMovie(userId);
            return Ok(values);
        }

        // Kullanıcının belirli statusdaki filmleri
        [HttpGet("user/{userId}/status/{status}")]
        public IActionResult GetUserMoviesByStatus(int userId, string status)
        {
            var values = _userMovieService.TGetUserMoviesByStatus(userId, status);
            return Ok(values);
        }

        // Film durumu sorgula
        [HttpGet("status")]
        public IActionResult GetMovieStatus([FromQuery] int userId, [FromQuery] int movieId)
        {
            var userMovie = _userMovieService.TGetUserMovie(userId, movieId);
            if (userMovie == null)
                return Ok(new { status = "" });

            return Ok(new { status = userMovie.Status });
        }

        // Film ekle veya güncelle
        [HttpPost]
        public IActionResult AddOrUpdateUserMovie([FromBody] UserMovieRequest request)
        {
            var result = _userMovieService.TAddOrUpdateUserMovie(
                request.UserId,
                request.MovieId,
                request.Status
            );

            if (result)
                return Ok(new { success = true, message = "Film durumu güncellendi" });

            return BadRequest(new { success = false, message = "İşlem başarısız" });
        }

        // Film sil
        [HttpDelete]
        public IActionResult RemoveUserMovie([FromQuery] int userId, [FromQuery] int movieId)
        {
            var result = _userMovieService.TRemoveUserMovie(userId, movieId);

            if (result)
                return Ok(new { success = true, message = "Film kaldırıldı" });

            return BadRequest(new { success = false, message = "İşlem başarısız" });
        }
    }

    public class UserMovieRequest
    {
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public string Status { get; set; }
    }
}
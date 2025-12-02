// Izle.WebUI/Models/UserMovie/UserMovie.cs
namespace Izle.WebUI.Models.UserMovie
{
    public class UserMovie
    {
        public int UserMovieId { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public string Status { get; set; }
        public DateTime AddedDate { get; set; }
        public Movie.Movie Movie { get; set; }
    }
}
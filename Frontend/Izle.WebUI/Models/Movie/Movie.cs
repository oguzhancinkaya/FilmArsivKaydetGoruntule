namespace Izle.WebUI.Models.Movie
{
	public class Movie
	{
		public int MovieId { get; set; }
		public string Title { get; set; }
		public string CoverImageUrl { get; set; }
		public string? Category { get; set; }
		public decimal Rating { get; set; }
		public string Description { get; set; }
		public DateTime ReleaseDate { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Izle.EntityLayer.Concrete
{
    public class UserMovie
    {
        public int UserMovieId { get; set; }
        public int UserId { get; set; }
        public int MovieId { get; set; }
        public string Status { get; set; } // "Watched", "Watching", "WatchLater"
        public DateTime AddedDate { get; set; }

        // Navigation properties
        public virtual AppUser AppUser { get; set; }
        public virtual Movie Movie { get; set; }
    }
}

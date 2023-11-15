using System.ComponentModel.DataAnnotations.Schema;

namespace CityExplorer.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int LandmarkId { get; set; }
        public Landmark Landmark { get; set; }
    }
}

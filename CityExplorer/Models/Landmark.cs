using System.ComponentModel.DataAnnotations.Schema;

namespace CityExplorer.Models
{
    public class Landmark
    {
        public int LandmarkId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string OpeningHours { get; set; }
        public string TourDuration { get; set; }
        public string Location { get; set; }

        [ForeignKey("City")]
        public int CityId { get; set; } 
        public string CityName { get; set; }

        public City City { get; set; }
        public List<Review> Reviews { get; set; }
        public List<LandmarkCategory> LandmarkCategories { get; set; }

    }
}

using CityExplorer.Models.Base;

namespace CityExplorer.Models
{
    public class UserLandmark : ModelBase
    {
        public string? AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int LandmarkId { get; set; }
        public Landmark Landmark { get; set; }


    }
}

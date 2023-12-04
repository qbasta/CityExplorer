using CityExplorer.Models.Base;

namespace CityExplorer.Models
{
    public class UserRoute : ModelBase
    {
        public string? AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public List<UserLandmark> Landmarks { get; set; } = new List<UserLandmark>();

    }
}
using CityExplorer.Models.Base;

namespace CityExplorer.Models
{
    public class UserLandmark : ModelBase
    {
        public int LandmarkId { get; set; }
        public Landmark Landmark { get; set; }

        public int UserLandmarkListId { get; set; }
        public UserLandmarkList UserLandmarkList { get; set; }
    }
}

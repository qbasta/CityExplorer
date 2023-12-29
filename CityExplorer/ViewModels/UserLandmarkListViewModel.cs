using CityExplorer.Models;

namespace CityExplorer.ViewModels
{
    public class UserLandmarkListViewModel
    {
        public List<UserLandmark> UserLandmarks { get; set; }
        public UserLandmarkList UserLandmarkList { get; set; }
        public int Likes { get; set; }
    }
}

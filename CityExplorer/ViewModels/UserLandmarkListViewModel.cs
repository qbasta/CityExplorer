using CityExplorer.Models;

namespace CityExplorer.ViewModels
{
    public class UserLandmarkListViewModel
    {
        public List<UserLandmarkList> UserLandmarkLists { get; set; }
        public List<UserLandmark> UserLandmarks { get; set; }
        public List<Landmark> Landmarks { get; set; }
        public int Likes { get; set; }
    }
}

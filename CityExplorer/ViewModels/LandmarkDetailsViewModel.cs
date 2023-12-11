using CityExplorer.Models;

namespace CityExplorer.ViewModels
{
    public class LandmarkDetailsViewModel
    {
        public IEnumerable<Review> Reviews { get; set; }
        public Landmark Landmark { get; set; }
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}

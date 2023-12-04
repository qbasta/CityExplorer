using CityExplorer.Models;

namespace CityExplorer.ViewModels
{
    public class LandmarkListViewModel
    {
        public IQueryable<Landmark> LandmarkList { get; set; }
        public int PageSize                      { get; set; }
        public int CurrentPage                   { get; set; }
        public int TotalPages                    { get; set; }
        public string? Term                      { get; set; }

    }
}

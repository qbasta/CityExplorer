using CityExplorer.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CityExplorer.ViewModels
{
    public class LandmarkListViewModel
    {
        public IQueryable<Landmark> LandmarkList { get; set; }
        public Landmark Landmark { get; set; }
        public int PageSize                      { get; set; }
        public int CurrentPage                   { get; set; }
        public int TotalPages                    { get; set; }
        public string? Term                      { get; set; }
        public string? NameFilter                { get; set; } 
        public List<int>? CategoryFilter         { get; set; } = new List<int>();
        public SelectList? CategoryList { get; set; }
    }
}

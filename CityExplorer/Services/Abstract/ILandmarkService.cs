using CityExplorer.Models;
using CityExplorer.ViewModels;

namespace CityExplorer.Services.Abstract
{
    public interface ILandmarkService
    {
        bool Add(Landmark landmark);
        bool Update(Landmark landmark);
        bool Delete(int id);
        Landmark GetById(int id);
        List<string> GetUniqueCities();
        List<string> GetUniqueCountries();
        LandmarkListViewModel List(string term = "", bool paging = false, int currentPage = 0,
                                   string nameFilter = "", List<int> categoryFilter = null, 
                                   string cityFilter = "", string countryFilter = "");
        List<int> GetCategoryByLandmarkId(int landmarkId);
        
    }
}
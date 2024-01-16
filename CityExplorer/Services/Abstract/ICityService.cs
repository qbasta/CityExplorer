using CityExplorer.Models;

namespace CityExplorer.Services.Abstract
{
    public interface ICityService
    {
        bool Add(City model);
        bool Update(City model);
        bool Delete(int id);
        City FindById(int id);
        IEnumerable<City> GetAll();
        City FindByCityAndCountry(string cityName, string countryName);
    }
}

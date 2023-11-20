using CityExplorer.Models;

namespace CityExplorer.Services.Abstract
{
    public interface ICategoryService
    {
        bool Add(Category model);
        bool Update(Category model);
        bool Delete(int id);
        Category FindById(int id);
        IEnumerable<Category> GetAll();
    }
}

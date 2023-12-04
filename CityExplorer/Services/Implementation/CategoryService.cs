using CityExplorer.Data;
using CityExplorer.Models;
using CityExplorer.Services.Abstract;

namespace CityExplorer.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _context;

        public CategoryService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public bool Add(Category model)
        {
            try
            {
                _context.Categories.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        
        public bool Update(Category model)
        {
            try
            {
                _context.Categories.Update(model);
                _context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var data = this.FindById(id);
                if (data == null)
                    return false;

                // Usuń powiązane LandmarkCategories
                _context.LandmarkCategories.RemoveRange(_context.LandmarkCategories.Where(lc => lc.CategoryId == data.Id));

                _context.Categories.Remove(data);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Category FindById(int id)
        {
            return _context.Categories.Find(id);
        }

        public IEnumerable<Category> GetAll()
        {
            return _context.Categories.ToList();
        }

    }
}

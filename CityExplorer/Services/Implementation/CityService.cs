using CityExplorer.Data;
using CityExplorer.Models;
using CityExplorer.Services.Abstract;

namespace CityExplorer.Services.Implementation
{
    public class CityService : ICityService
    {
        private readonly ApplicationDbContext _context;

        public CityService(ApplicationDbContext context)
        {
            _context = context;
        }
        
        public bool Add(City model)
        {
            try
            {
                _context.Cities.Add(model);
                _context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        
        public bool Update(City model)
        {
            try
            {
                _context.Cities.Update(model);
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
                _context.Cities.Remove(data);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    
        public City FindById(int id)
        {
           return _context.Cities.Find(id);
        }

        public IEnumerable<City> GetAll()
        {
            return _context.Cities.ToList();
        }
    }
}

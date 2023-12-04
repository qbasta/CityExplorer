using CityExplorer.Data;
using CityExplorer.Models;
using CityExplorer.Services.Abstract;

namespace CityExplorer.Services.Implementation
{
    public class UserRouteService : IUserRouteService
    {
        private readonly ApplicationDbContext _context;

        public UserRouteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool SaveUserRoute(string userId, List<int> landmarkIds)
        {
            try
            {
                var userRoute = new UserRoute
                {
                    AppUserId = userId
                };

                foreach (var landmarkId in landmarkIds)
                {
                    userRoute.Landmarks.Add(new UserLandmark
                    {
                        LandmarkId = landmarkId
                    });
                }

                _context.UserRoutes.Add(userRoute);
                _context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
    


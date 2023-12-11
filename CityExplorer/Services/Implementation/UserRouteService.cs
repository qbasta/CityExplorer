using CityExplorer.Data;
using CityExplorer.Models;
using CityExplorer.Services.Abstract;
using Microsoft.EntityFrameworkCore;

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
                var user = _context.Users.Find(userId);
                if (user == null)
                {
                    return false;
                }

                var userRoute = new UserRoute
                {
                    AppUserId = userId,
                    AppUser = user
                };

                foreach (var landmarkId in landmarkIds)
                {
                    var landmark = _context.Landmarks.Find(landmarkId);
                    if (landmark == null)
                    {
                        continue;
                    }

                    userRoute.Landmarks.Add(new UserLandmark
                    {
                        LandmarkId = landmarkId,
                        Landmark = landmark,
                        AppUserId = userId,
                        AppUser = user
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

        public bool DeleteUserRoute(string userId, int routeId)
        {
            var userRoute = _context.UserRoutes.Include(ur => ur.Landmarks).FirstOrDefault(ur => ur.AppUserId == userId && ur.Id == routeId);
            if (userRoute == null)
            {
                return false;
            }

            // Usuń wszystkie UserLandmarks, które odwołują się do tej trasy
            _context.UserLandmarks.RemoveRange(userRoute.Landmarks);

            _context.UserRoutes.Remove(userRoute);
            _context.SaveChanges();

            return true;
        }


        public List<UserRoute> GetUserRoutes(string userId)
        {
            // Zwróć listę tras użytkownika z bazy danych
            // Na przykład:
            return _context.UserRoutes.Include(ur => ur.Landmarks).ThenInclude(ul => ul.Landmark).Where(ur => ur.AppUserId == userId).ToList();
        }
    }
}



using CityExplorer.Data;
using CityExplorer.Models;
using CityExplorer.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CityExplorer.Services.Implementation
{
    public class UserLandmarkService : IUserLandmarkService 
    {
        private readonly ApplicationDbContext _context;

        public UserLandmarkService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Landmark> GetUserLandmarks(string userId, bool includeReviews = false)
        {
            var query = _context.UserLandmarks
                        .Where(ul => ul.AppUserId == userId);

            if (includeReviews)
            {
                // Move Include before Select
                query = query.Include(ul => ul.Landmark.Reviews);
            }

            // Now apply the Select method
            var landmarks = query.Select(ul => ul.Landmark).ToList();

            return landmarks;
        }

        public bool RemoveFromUserLandmarks(string userId, int landmarkId)
        {
            try
            {
                var userLandmark = _context.UserLandmarks
                    .FirstOrDefault(ul => ul.AppUserId == userId && ul.LandmarkId == landmarkId);

                if (userLandmark != null)
                {
                    _context.UserLandmarks.Remove(userLandmark);
                    _context.SaveChanges();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool AddToUserLandmarks(string userId, int landmarkId)
        {
            try
            {
                // Sprawdź, czy zabytek już istnieje na liście użytkownika
                var existingUserLandmark = _context.UserLandmarks
                    .FirstOrDefault(ul => ul.AppUserId == userId && ul.LandmarkId == landmarkId);

                if (existingUserLandmark != null)
                {
                    // Zabytek już istnieje na liście użytkownika, nie dodawaj ponownie
                    return false;
                }

                var userLandmark = new UserLandmark
                {
                    AppUserId = userId,
                    LandmarkId = landmarkId
                };

                _context.UserLandmarks.Add(userLandmark);
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
    


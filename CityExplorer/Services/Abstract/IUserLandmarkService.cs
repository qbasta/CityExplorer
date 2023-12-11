using CityExplorer.Models;

namespace CityExplorer.Services.Abstract
{
    public interface IUserLandmarkService
    {
        bool AddToUserLandmarks(string userId, int landmarkId);
        bool RemoveFromUserLandmarks(string userId, int landmarkId);
        List<Landmark> GetUserLandmarks(string userId, bool includeReviews);
    }
}
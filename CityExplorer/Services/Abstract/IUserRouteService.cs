using CityExplorer.Models;

namespace CityExplorer.Services.Abstract
{
    public interface IUserRouteService
    {
        bool SaveUserRoute(string userId, List<int> landmarkIds);
        bool DeleteUserRoute(string userId, int routeId);
        List<UserRoute> GetUserRoutes(string userId);
    }
}

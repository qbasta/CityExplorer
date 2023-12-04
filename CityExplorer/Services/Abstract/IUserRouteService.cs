namespace CityExplorer.Services.Abstract
{
    public interface IUserRouteService
    {
        bool SaveUserRoute(string userId, List<int> landmarkIds);
    }
}

using CityExplorer.Models;
using CityExplorer.ViewModels;

namespace CityExplorer.Services.Abstract
{
    public interface IUserLandmarkListService
    {
        bool AddToUserList(string userId, int landmarkId);
        bool SaveUserList(string userId, string name, string description);
        bool MakeListPublic(string userId, int listId);
        bool? RemoveFromUserList(string userId, int landmarkId);
        bool? RemoveSavedUserList(string userId, int listId);
        UserLandmarkList GetUserList(string userId);
        UserLandmarkList GetRouteDetails(string userId, int routeId);
        List<UserLandmarkList> GetSavedUserLists(string userId);
        UserLandmarkListViewModel GetPublicUserLists();
    }
}

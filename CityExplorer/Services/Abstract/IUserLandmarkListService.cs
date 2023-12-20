using CityExplorer.Models;

namespace CityExplorer.Services.Abstract
{
    public interface IUserLandmarkListService
    {
        bool AddToUserList(string userId, int landmarkId);
        bool SaveUserList(string userId, string name);
        bool? RemoveFromUserList(string userId, int landmarkId);
        bool? RemoveSavedUserList(string userId, int listId);
        UserLandmarkList GetUserList(string userId);
        List<UserLandmarkList> GetSavedUserLists(string userId);
    }
}

﻿using CityExplorer.Data;
using CityExplorer.Models;
using CityExplorer.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CityExplorer.Services.Implementation
{
    public class UserLandmarkListService : IUserLandmarkListService
    {
        private readonly ApplicationDbContext _context;

        public UserLandmarkListService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool AddToUserList(string userId, int landmarkId)
        {
            var userLandmarkList = _context.UserLandmarkLists
                .Include(ull => ull.UserLandmarks)
                .FirstOrDefault(ull => ull.AppUserId == userId && !ull.IsSaved);

            if (userLandmarkList == null)
            {
                userLandmarkList = new UserLandmarkList { AppUserId = userId, Name = "Domyślna nazwa" };
                _context.UserLandmarkLists.Add(userLandmarkList);
                _context.SaveChanges();
            }

            if (userLandmarkList.UserLandmarks.Count >= 7)
            {
                return false;
            }

            // Sprawdź, czy zabytek już istnieje na liście
            var existingUserLandmark = userLandmarkList.UserLandmarks
                .FirstOrDefault(ul => ul.LandmarkId == landmarkId);
            if (existingUserLandmark != null)
            {
                // Zabytek już istnieje na liście, więc nie dodawaj go ponownie
                return false;
            }

            var userLandmark = new UserLandmark
            {
                LandmarkId = landmarkId,
                UserLandmarkListId = userLandmarkList.Id
            };

            _context.UserLandmarks.Add(userLandmark);
            _context.SaveChanges();

            return true;
        }

        public bool? RemoveFromUserList(string userId, int landmarkId)
        {
            var userLandmarkList = _context.UserLandmarkLists
                .Include(ull => ull.UserLandmarks)
                .FirstOrDefault(ull => ull.AppUserId == userId);

            if (userLandmarkList == null)
            {
                // Lista użytkownika nie istnieje
                return null;
            }

            var userLandmark = userLandmarkList.UserLandmarks
                .FirstOrDefault(ul => ul.LandmarkId == landmarkId);

            if (userLandmark == null)
            {
                // Zabytek nie istnieje na liście użytkownika
                return null;
            }

            _context.UserLandmarks.Remove(userLandmark);
            _context.SaveChanges();

            return true;
        }

        public bool? RemoveSavedUserList(string userId, int listId)
        {
            var userLandmarkList = _context.UserLandmarkLists
                .Include(ull => ull.UserLandmarks)
                .FirstOrDefault(ull => ull.AppUserId == userId && ull.Id == listId && ull.IsSaved);

            if (userLandmarkList == null)
            {
                // Lista użytkownika nie istnieje
                return null;
            }

            _context.UserLandmarkLists.Remove(userLandmarkList);
            _context.SaveChanges();

            return true;
        }

        public bool SaveUserList(string userId, string name)
        {
            var userLandmarkList = _context.UserLandmarkLists
                .Include(ull => ull.UserLandmarks)
                .FirstOrDefault(ull => ull.AppUserId == userId && !ull.IsSaved);

            if (userLandmarkList != null)
            {
                userLandmarkList.IsSaved = true;
                userLandmarkList.Name = name;
                userLandmarkList.CreatedAt = DateTime.Now;
                _context.SaveChanges();
            }

            // Tworzenie nowej listy prywatnej
            var newUserLandmarkList = new UserLandmarkList { AppUserId = userId, Name = "Domyślna nazwa" };
            _context.UserLandmarkLists.Add(newUserLandmarkList);
            _context.SaveChanges();

            return true;
        }

        public UserLandmarkList GetUserList(string userId)
        {
            var userList = _context.UserLandmarkLists
                .Include(ull => ull.UserLandmarks)
                .ThenInclude(ul => ul.Landmark)
                .FirstOrDefault(ull => ull.AppUserId == userId && !ull.IsSaved);

            // Jeśli userList jest null, zwróć nową, pustą listę
            return userList ?? new UserLandmarkList { UserLandmarks = new List<UserLandmark>() };
        }

        public List<UserLandmarkList> GetSavedUserLists(string userId)
        {
            return _context.UserLandmarkLists
                .Include(ull => ull.UserLandmarks)
                .ThenInclude(ul => ul.Landmark)
                .Where(ull => ull.AppUserId == userId && ull.IsSaved)
                .ToList();
        }
    }
}
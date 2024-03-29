﻿using CityExplorer.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CityExplorer.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserLandmarkListService _userLandmarkListService;

        public UserController(IUserLandmarkListService userLandmarkListService)
        {
            _userLandmarkListService = userLandmarkListService;
        }

        [HttpPost]
        public IActionResult AddToUserLandmarks(int landmarkId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var success = _userLandmarkListService.AddToUserList(userId, landmarkId);

            return Json(new { success });
        }

        [HttpPost]
        public IActionResult SaveUserList(string name, string description)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var success = _userLandmarkListService.SaveUserList(userId, name, description);

            if (success)
            {
                return RedirectToAction("SavedUserLists");
            }
            else
            {
                // Obsłuż błąd zapisu tutaj
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult RemoveFromUserLandmarks(int landmarkId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _userLandmarkListService.RemoveFromUserList(userId, landmarkId);

            if (result == null)
            {
                return Json(new { success = false, message = "Zabytek nie istnieje na liście użytkownika." });
            }
            else if (!result.Value)
            {
                return Json(new { success = false, message = "Nie udało się usunąć zabytku." });
            }

            return Json(new { success = true });
        }

        [HttpPost]
        public IActionResult RemoveSavedUserList(int listId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _userLandmarkListService.RemoveSavedUserList(userId, listId);

            if (result == null)
            {
                return Json(new { success = false, message = "Lista nie istnieje." });
            }
            else if (!result.Value)
            {
                return Json(new { success = false, message = "Nie udało się usunąć listy." });
            }

            return RedirectToAction("SavedUserLists");
        }
        
        [HttpPost]
        public IActionResult MakeUserListPublic(int listId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var success = _userLandmarkListService.MakeListPublic(userId, listId);

            if (success)
            {
                return RedirectToAction("SavedUserLists");
            }
            else
            {
                // Obsłuż błąd tutaj
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult RouteDetails(int routeId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var route = _userLandmarkListService.GetRouteDetails(userId, routeId);

            if (route == null)
            {
                return View("Error");
            }

            return View(route);
        }

        public IActionResult PublicUserLists()
        {
            var viewModel = _userLandmarkListService.GetPublicUserLists();

            return View(viewModel);
        }

        public IActionResult UserList()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userList = _userLandmarkListService.GetUserList(userId);

            return View(userList);
        }

        public IActionResult SavedUserLists()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var savedUserLists = _userLandmarkListService.GetSavedUserLists(userId);

            return View(savedUserLists);
        }
    }
}

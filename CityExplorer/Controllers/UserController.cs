using CityExplorer.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CityExplorer.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserLandmarkService _userLandmarkService;
        private readonly IUserRouteService _userRouteService;

        public UserController(IUserLandmarkService userLandmarkService, IUserRouteService userRouteService)
        {
            _userLandmarkService = userLandmarkService;
            _userRouteService = userRouteService;
        }

        [HttpPost]
        public IActionResult AddToUserLandmarks(int landmarkId)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Sprawdź, czy użytkownik ma już 7 zabytków na liście
                var userLandmarks = _userLandmarkService.GetUserLandmarks(userId, includeReviews: true);
                if (userLandmarks.Count >= 7)
                {
                    return Json(new { success = false, message = "Osiągnięto maksymalną liczbę zabytków na liście." });
                }

                // Dodaj zabytek do listy użytkownika
                var result = _userLandmarkService.AddToUserLandmarks(userId, landmarkId);

                if (result)
                {
                    return Json(new { success = true });
                }
                else
                {
                    return Json(new { success = false, message = "Nie udało się dodać zabytku do listy użytkownika." });
                }
            }
            catch (Exception ex)
            {
                // Zaloguj wyjątek
                Console.WriteLine(ex.Message);
                return Json(new { success = false, message = "Wystąpił błąd serwera." });
            }
        }

        [HttpPost]
        public IActionResult RemoveFromUserLandmarks(int landmarkId)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var result = _userLandmarkService.RemoveFromUserLandmarks(userId, landmarkId);

                if (result)
                {
                    // Pobierz zaktualizowaną listę zabytków użytkownika
                    var updatedUserLandmarks = _userLandmarkService.GetUserLandmarks(userId, includeReviews: true);

                    // Zaktualizuj widok HTML, przekazując zaktualizowaną listę do widoku
                    return View("UserLandmarks", updatedUserLandmarks);
                }
                else
                {
                    return Json(new { success = false, message = "Nie udało się usunąć zabytku z listy użytkownika." });
                }
            }
            catch (Exception ex)
            {
                // Zaloguj wyjątek
                Console.WriteLine(ex.Message);
                return Json(new { success = false, message = "Wystąpił błąd serwera." });
            }
        }


        [HttpPost]
        public IActionResult SaveUserRoute(List<int> landmarkIds, string routeName)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _userRouteService.SaveUserRoute(userId, landmarkIds, routeName);

            if (result)
            {
                // Usuń zabytki z listy użytkownika po zapisaniu trasy
                foreach (var landmarkId in landmarkIds)
                {
                    _userLandmarkService.RemoveFromUserLandmarks(userId, landmarkId);
                }
            }

            // Pobierz zaktualizowaną listę zabytków użytkownika
            var updatedUserLandmarks = _userLandmarkService.GetUserLandmarks(userId, includeReviews: true); // lub true, w zależności od Twoich potrzeb

            // Zaktualizuj widok HTML, przekazując zaktualizowaną listę do widoku
            return View("UserLandmarks", updatedUserLandmarks);
        }

        [HttpPost]
        public IActionResult DeleteUserRoute(int routeId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _userRouteService.DeleteUserRoute(userId, routeId);

            if (result)
            {
                // Pobierz zaktualizowaną listę tras użytkownika
                var updatedUserRoutes = _userRouteService.GetUserRoutes(userId);

                // Zaktualizuj widok HTML, przekazując zaktualizowaną listę do widoku
                return View("UserRoutes", updatedUserRoutes);
            }
            else
            {
                // Obsłuż błąd, na przykład wyświetl komunikat o błędzie
                return View("Error");
            }
        }

        public IActionResult UserLandmarks()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userLandmarks = _userLandmarkService.GetUserLandmarks(userId, includeReviews: true); // Provide the required argument
            return View(userLandmarks);
        }

        public IActionResult UserRoutes()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userRoutes = _userRouteService.GetUserRoutes(userId); // Ta metoda musi być dodana do interfejsu IUserRouteService i zaimplementowana w UserRouteService
            return View(userRoutes);
        }

    }
}
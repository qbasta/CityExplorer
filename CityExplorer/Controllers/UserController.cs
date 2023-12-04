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
                var userLandmarks = _userLandmarkService.GetUserLandmarks(userId);
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
                    var updatedUserLandmarks = _userLandmarkService.GetUserLandmarks(userId);

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
        public IActionResult SaveUserRoute(List<int> landmarkIds)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = _userRouteService.SaveUserRoute(userId, landmarkIds);

            // Obsługa wyniku (np. zwracanie JSON z informacją o sukcesie lub błędzie)

            return Json(new { success = result });
        }

        public IActionResult UserLandmarks()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userLandmarks = _userLandmarkService.GetUserLandmarks(userId);
            return View(userLandmarks);
        }


    }
}
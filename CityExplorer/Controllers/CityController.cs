using CityExplorer.Models;
using CityExplorer.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CityExplorer.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityService _cityService;

        public CityController(ICityService cityService)
        {
            _cityService = cityService;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(City model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _cityService.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(GetAll));
            }
            TempData["msg"] = "Error has occurred on the server side";
            return View(model);
        }

        public IActionResult Update(int id)
        {
            var record = _cityService.FindById(id);
            return View(record);
        }

        [HttpPost]
        public IActionResult Update(City model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _cityService.Update(model);
            if (result)
            {
                TempData["msg"] = "Updated Successfully";
                return RedirectToAction(nameof(GetAll));
            }
            TempData["msg"] = "Error has occurred on the server side";
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var result = _cityService.Delete(id);
            return RedirectToAction("GetAll");
        }

        public IActionResult GetAll()
        {
            var data = _cityService.GetAll();
            return View(data);
        }
    }
}

using CityExplorer.Models;
using CityExplorer.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CityExplorer.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
    
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Category model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Sprawdź, czy kategoria o tej samej nazwie już istnieje
            var existingCategory = _categoryService.GetAll().FirstOrDefault(c => c.Name == model.Name);
            if (existingCategory != null)
            {
                TempData["msg"] = "Kategoria o tej nazwie już istnieje";
                return View(model);
            }

            var result = _categoryService.Add(model);
            if (result)
            {
                TempData["msg"] = "Pomyślnie dodano nową kategorię";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "Wystąpił błąd po stronie serwera";
            return View(model);
        }


        public IActionResult Update(int id)
        {
            var record = _categoryService.FindById(id);
            return View(record);
        }

        [HttpPost]
        public IActionResult Update(Category model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = _categoryService.Update(model);
            if (result)
            {
                TempData["msg"] = "Pomyślnie zaktualizowano kategorię";
                return RedirectToAction(nameof(Update));
            }
            TempData["msg"] = "Wystąpił błąd po stronie serwera";
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var result = _categoryService.Delete(id);
            return RedirectToAction("GetAll");
        }

        public IActionResult GetAll()
        {

            var data = _categoryService.GetAll();
            return View(data);
        }

    }
}
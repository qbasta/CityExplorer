using CityExplorer.Data;
using CityExplorer.Models;
using CityExplorer.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CityExplorer.Controllers
{
    public class LandmarkController : Controller
    {
        private readonly ILandmarkService _landmarkService;
        private readonly ICategoryService _categoryService;
        private readonly ICityService _cityService;
        private readonly IFileService _fileService;
        //private readonly IReviewService _reviewService;
        private readonly ApplicationDbContext _context;

        public LandmarkController(ILandmarkService landmarkService, ICategoryService categoryService, ICityService cityService, IFileService fileService, ApplicationDbContext context)
        {
            _landmarkService = landmarkService;
            _categoryService = categoryService;
            _cityService = cityService;
            _fileService = fileService;
            _context = context;
        }

        public IActionResult Add()
        {
            var model = new Landmark();
            model.CategoryList = _categoryService.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });
            ViewData["City"] = new SelectList(_cityService.GetAll(), "Id", "Name");
            ViewData["Country"] = new SelectList(_cityService.GetAll(), "Id", "Country");
            return View(model);
        }

        [HttpPost]
        public IActionResult Add(Landmark model)
        {

            ViewData["City"] = new SelectList(_cityService.GetAll(), "Id", "Name", model.CityId);
            ViewData["Country"] = new SelectList(_cityService.GetAll(), "Id", "Country", model.CityId);

            model.CategoryList = _categoryService.GetAll().Select(c => new SelectListItem
            {
                Text = c.Name,
                Value = c.Id.ToString()
            });

            if(!ModelState.IsValid)
            {
                return View(model);
            }
            if(model.ImageFile != null)
            {
                var fileResult = _fileService.SaveImage(model.ImageFile);
                if(fileResult.Item1 == 0)
                {
                    TempData["msg"] = "Plik nie mógł zostać zapisany";
                    return View(model);
                }
                var imageName = fileResult.Item2;
                model.ImagePath = imageName;
            }
            var result = _landmarkService.Add(model);
            if(result)
            {
                TempData["msg"] = "Zapisano pomyślnie";
                return RedirectToAction(nameof(Add));
            }
            else
            {
                TempData["msg"] = "Wystąpił błąd podczas zapisu";
                return View(model);
            }
        }

        public IActionResult Edit(int id)
        {
           var model = _landmarkService.GetById(id);
           var selectedCategories = _landmarkService.GetCategoryByLandmarkId(model.Id);
           MultiSelectList multiCategoryList = new MultiSelectList(_categoryService.GetAll(), "Id", "Name", selectedCategories);
           model.MultiCategoryList = multiCategoryList;
           ViewData["City"] = new SelectList(_cityService.GetAll(), "Id", "Name", model.CityId);
           ViewData["Country"] = new SelectList(_cityService.GetAll(), "Id", "Country", model.CityId);
           return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Landmark model)
        {
            ViewData["City"] = new SelectList(_cityService.GetAll(), "Id", "Name", model.CityId);
            ViewData["Country"] = new SelectList(_cityService.GetAll(), "Id", "Country", model.CityId);
            var selectedCategories = _landmarkService.GetCategoryByLandmarkId(model.Id);
            MultiSelectList multiCategoryList = new MultiSelectList(_categoryService.GetAll(), "Id", "Name", selectedCategories);
            model.MultiCategoryList = multiCategoryList;
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            if(model.ImageFile != null)
            {
                var fileResult = _fileService.SaveImage(model.ImageFile);
                if(fileResult.Item1 == 0)
                {
                    TempData["msg"] = "Plik nie mógł zostać zapisany";
                    return View(model);
                }
                var imageName = fileResult.Item2;
                model.ImagePath = imageName;
            }
            var result = _landmarkService.Update(model);
            if(result)
            {
                TempData["msg"] = "Zapisano pomyślnie";
                return RedirectToAction(nameof(LandmarkList));
            }
            else
            {
                TempData["msg"] = "Wystąpił błąd podczas zapisu";
                return View(model);
            }

        }

        public IActionResult Details(int? id)
        {
            if (id == null || _context.Landmarks == null)
            {
                return NotFound();
            }

            var landmark = _context.Landmarks
                .Include(l => l.Country)
                .FirstOrDefault(m => m.Id == id);

            if (landmark == null)
            {
                return NotFound();
            }
            return View(landmark);
            
                

        }

        public IActionResult LandmarkList()
        {
            var data = _landmarkService.List();
            return View(data);
        }

        public IActionResult Delete(int id)
        {
            var result = _landmarkService.Delete(id);
            return RedirectToAction(nameof(LandmarkList));
        }

    }
}

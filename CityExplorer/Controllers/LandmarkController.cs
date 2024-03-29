﻿using CityExplorer.Data;
using CityExplorer.Models;
using CityExplorer.Services.Abstract;
using CityExplorer.Services.Implementation;
using CityExplorer.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

    namespace CityExplorer.Controllers
    {
        public class LandmarkController : Controller
        {
            private readonly ILandmarkService _landmarkService;
            private readonly ICategoryService _categoryService;
            private readonly ICityService _cityService;
            private readonly IFileService _fileService;
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

            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                if (model.ImageFile != null)
                {
                    var fileResult = _fileService.SaveImage(model.ImageFile);
                    if (fileResult.Item1 == 0)
                    {
                        TempData["msg"] = "Plik nie mógł zostać zapisany";
                        return View(model);
                    }
                    var imageName = fileResult.Item2;
                    model.ImagePath = imageName;
                }

                var result = _landmarkService.Add(model);
                if (result)
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
            catch (Exception ex)
            {
                TempData["error"] = $"Wystąpił błąd: {ex.Message}";
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


            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult AddReview(Review model)
            {
                try
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        model.Date = DateTime.Now;

                        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                        var user = _context.Users.Find(userId);

                        // Set the user role for the model review
                        model.AppUser = user;

                        _context.Reviews.Add(model);
                        _context.SaveChanges();

                        // Usunięcie linii kodu, która dodaje żądanie typu "ReviewId" do tożsamości użytkownika
                        // var reviewIdClaim = new Claim("ReviewId", model.Id.ToString());
                        // User.AddIdentity(new ClaimsIdentity(new[] { reviewIdClaim }));

                        TempData["msg"] = "Review added successfully.";
                    }

                    return RedirectToAction("Details", new { id = model.LandmarkId });
                }
                catch (DbUpdateException ex)
                {
                    TempData["msg"] = "An error occurred while saving the review.";
                    return RedirectToAction("Details", new { id = model.LandmarkId });
                }
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult DeleteReview(Review model)
            {
                var review = _context.Reviews.Find(model.Id);

                // Sprawdzenie, czy zalogowany użytkownik to autor recenzji lub administrator
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var isAdmin = User.IsInRole("Admin");
                if (User.Identity.IsAuthenticated && (review.AppUserId == userId || isAdmin))
                {
                    _context.Reviews.Remove(review);
                    _context.SaveChanges();
                    TempData["msg"] = "Recenzja została usunięta pomyślnie.";
                }
                else
                {
                    TempData["msg"] = "Nie masz uprawnień do usunięcia tej recenzji.";
                }

                return RedirectToAction("Details", new { id = model.LandmarkId });
            }

        public IActionResult Details(int? id, int page = 1, string tag = null)
        {
            if (id == null || _context.Landmarks == null)
            {
                return NotFound();
            }

            var landmark = _context.Landmarks
                .Include(l => l.City)
                .Include(l => l.Reviews)
                .ThenInclude(l => l.AppUser)// Include reviews
                .FirstOrDefault(m => m.Id == id);

            if (landmark == null)
            {
                return NotFound();
            }

            int pageSize = 5; // ustal liczbę recenzji na stronę
            var reviews = landmark.Reviews.Where(r => tag == null || r.Tag == tag).Skip((page - 1) * pageSize).Take(pageSize); // pobierz odpowiednie recenzje

            var model = new LandmarkDetailsViewModel
            {
                Landmark = landmark,
                Reviews = reviews,
                PageSize = pageSize,
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling(landmark.Reviews.Count() / (double)pageSize)
            };

            // Pobieramy kategorie zabytku
            var landmarkCategories = _context.LandmarkCategories
                .Where(lc => lc.LandmarkId == id)
                .Include(lc => lc.Category)
                .ToList();

            // Przekazujemy listę kategorii do widoku za pomocą ViewBag
            ViewBag.CategoryList = landmarkCategories.Select(lc => new SelectListItem
            {
                Text = lc.Category.Name,
                Value = lc.Category.Id.ToString()
            });

            return View(model);
        }



        public IActionResult LandmarkList(string searchTerm, string cityFilter, string countryFilter)
            {
                var cities = _landmarkService.GetUniqueCities();
                var countries = _landmarkService.GetUniqueCountries();
                var data = _landmarkService.List(term: searchTerm, cityFilter: cityFilter, countryFilter: countryFilter);
                data.Cities = cities;
                data.Countries = countries;
                return View(data);
            }


            public IActionResult Delete(int id)
            {
                var result = _landmarkService.Delete(id);
                return RedirectToAction(nameof(LandmarkList));
            }

        }
    }

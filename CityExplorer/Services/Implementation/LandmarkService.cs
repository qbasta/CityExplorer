﻿using CityExplorer.Data;
using CityExplorer.Models;
using CityExplorer.Services.Abstract;
using CityExplorer.ViewModels;
using System.Linq;

namespace CityExplorer.Services.Implementation
{
    public class LandmarkService : ILandmarkService
    {
        private readonly ApplicationDbContext _context;
        public LandmarkService(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool Add(Landmark model)
        {
            try
            {
                _context.Landmarks.Add(model);
                _context.SaveChanges();
                foreach(var categoryId in model.Categories)
                {
                    var landmarkCategory = new LandmarkCategory
                    {
                        LandmarkId = model.Id,
                        CategoryId = categoryId
                    };
                    _context.LandmarkCategories.Add(landmarkCategory);
                }
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public bool Delete(int id)
        {
            try
            {
                var data = this.GetById(id);
                if (data == null)
                    return false;
                var landmarkCategories = _context.LandmarkCategories.Where(lc => lc.LandmarkId == data.Id);
                foreach(var landmarkCategory in landmarkCategories)
                {
                    _context.LandmarkCategories.Remove(landmarkCategory);
                }
                _context.Landmarks.Remove(data);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        
        public Landmark GetById(int id)
        {
            return _context.Landmarks.Find(id);
        }
        
        public LandmarkListViewModel List(string term = "", bool paging = false, int currentPage = 0)
        {
            var data = new LandmarkListViewModel();
            var list = _context.Landmarks.ToList();

            if(!string.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                list = list.Where(l => l.Name.ToLower().StartsWith(term)).ToList();
            }

            if (paging)
            {
                int pageSize = 5;
                int count = list.Count;
                int TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                list = list.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                data.PageSize = pageSize;
                data.CurrentPage = currentPage;
                data.TotalPages = TotalPages;
            }

            foreach(var landmark in list)
            {
                var categories = (from category in _context.Categories
                                  join lc in _context.LandmarkCategories
                                  on category.Id equals lc.CategoryId
                                  where lc.LandmarkId == landmark.Id
                                  select category.Name).ToList();
                var categoryNames = string.Join(", ", categories);
                landmark.CategoryNames = categoryNames;
            }
            data.LandmarkList = list.AsQueryable();
            return data;
        }

        public bool Update(Landmark model)
        {
            try
            {
                var categoriesToDelete = _context.LandmarkCategories.Where(lc => lc.LandmarkId == model.Id && !model.Categories.Contains(lc.CategoryId)).ToList();
                foreach(var lCategory in categoriesToDelete)
                {
                    _context.LandmarkCategories.Remove(lCategory);
                }
                foreach(int categoryId in model.Categories)
                {
                    var landmarkCategory = _context.LandmarkCategories.FirstOrDefault(lc => lc.LandmarkId == model.Id && lc.CategoryId == categoryId);
                    if(landmarkCategory == null)
                    {
                        landmarkCategory = new LandmarkCategory
                        {
                            LandmarkId = model.Id,
                            CategoryId = categoryId
                        };
                        
                    }
                    _context.LandmarkCategories.Add(landmarkCategory);
                }
                _context.Landmarks.Update(model);
                _context.SaveChanges();
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<int> GetCategoryByLandmarkId(int landmarkId)
        {
            var categoryIds = _context.LandmarkCategories.Where(lc => lc.LandmarkId == landmarkId).Select(lc => lc.CategoryId).ToList();
            return categoryIds;
        }

    }
}
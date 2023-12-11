﻿using System.ComponentModel.DataAnnotations.Schema;
using CityExplorer.Models.Base;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CityExplorer.Models;

public class Landmark : ModelBase
{
    public string  Name         { get; set; } = "Unknown";
    public string? Description  { get; set; }
    public string? ImagePath    { get; set; }
    public string? OpeningHours { get; set; }
    public string? TourDuration { get; set; } 
    public string? Location     { get; set; }

    [NotMapped]
    public IFormFile? ImageFile { get; set; }

    [ForeignKey("City")]
    public int CityId           { get; set; }
    public City? City        { get; set; }
    
    [NotMapped]
    public List<int>? Categories                        { get; set; }
    [NotMapped]
    public IEnumerable<SelectListItem>? CategoryList    { get; set; }
    [NotMapped]
    public MultiSelectList? MultiCategoryList           { get; set; }
    [NotMapped]
    public string? CategoryNames                        { get; set; }

    public List<Review>           Reviews               { get; set; } = new();

    public double AverageRating
    {
        get
        {
            var reviewRatings = Reviews.Where(r => r.Tag == "recenzja").Select(r => r.Rating);
            if (reviewRatings.Any())
            {
                return Math.Round(reviewRatings.Average(), 2);
            }
            return 0;
        }
    }
}

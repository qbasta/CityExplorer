using System.ComponentModel.DataAnnotations.Schema;
using CityExplorer.Models.Base;

namespace CityExplorer.Models;

public class Landmark : ModelBase
{
    public string  Name         { get; set; } = "Unknown";
    public string? Description  { get; set; }
    public string? ImagePath    { get; set; }
    public string? OpeningHours { get; set; }
    public string? TourDuration { get; set; }
    public string? Location     { get; set; }

    [ForeignKey("City")]
    public int CityId { get; set; }
    
    public City? City { get; set; }

    public List<LandmarkCategory> LandmarkCategories { get; set; } = new();
    public List<Review>           Reviews            { get; set; } = new();
}

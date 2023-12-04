using CityExplorer.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace CityExplorer.Models;

public class City : ModelBase
{
    [Required]
    public string Name { get; set; } = "Unknown City";
    [Required]
    public string Country { get; set; } = "Unknown Country";

    public List<Landmark> Landmarks { get; set; } = new();
}

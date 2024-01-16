using System.ComponentModel.DataAnnotations;
using CityExplorer.Models.Base;

namespace CityExplorer.Models
{
    public class Category : ModelBase
    {
        [Required]
        public string Name { get; set; } = "Unknown";
        public List<LandmarkCategory> LandmarkCategories { get; set; } = new();
    }
}
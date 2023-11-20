using System.ComponentModel.DataAnnotations;

namespace CityExplorer.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        public string Name { get; set; }

        public List<LandmarkCategory> LandmarkCategories { get; set; }
    }
}
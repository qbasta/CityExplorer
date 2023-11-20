using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CityExplorer.Models
{
    public class City
    {

        [Key]
        public int CityId { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Country { get; set; }

        public List<Landmark> Landmarks { get; set; }

        public City()
        {
            Landmarks = new List<Landmark>();
        }
    }
}

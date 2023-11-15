using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CityExplorer.Models
{
    public class City
    {

        [Key]
        public int CityId { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        [InverseProperty("City")]
        public List<Landmark> Landmarks { get; set; }

    }
}

using System.ComponentModel.DataAnnotations.Schema;
using CityExplorer.Models.Base;

namespace CityExplorer.Models
{
    public class Review : ModelBase
    {
        public string?  Text     { get; set; }
        public int      Rating   { get; set; }
        public DateTime Date     { get; set; }
        public string?  Tag      { get; set; }


        public required string  AppUserId { get; set; }
        public required AppUser AppUser   { get; set; }

        public int LandmarkId             { get; set; }
        public required Landmark Landmark { get; set; }
    }
}

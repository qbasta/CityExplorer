namespace CityExplorer.Models
{
    public class LandmarkCategory
    {
        public int LandmarkId { get; set; }
        public Landmark Landmark { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

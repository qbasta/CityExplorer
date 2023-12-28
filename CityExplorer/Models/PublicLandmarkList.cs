namespace CityExplorer.Models
{
    public class PublicLandmarkList
    {
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<UserLandmark> UserLandmarks { get; set; }
        public List<Review> Reviews { get; set; } = new();
        public int Likes { get; set; } = 0;
    }
}

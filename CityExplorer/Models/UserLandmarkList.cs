using CityExplorer.Models.Base;

namespace CityExplorer.Models
{
    public class UserLandmarkList : ModelBase
    {
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public bool IsSaved { get; set; } = false;
        public bool IsPublic { get; set; } = false;
        public List<UserLandmark> UserLandmarks { get; set; } = new();

        public string Name { get; set; } = "Domyślna nazwa";
        public DateTime CreatedAt { get; set; } = DateTime.Now;       
        public string SharedBy { get; set; }
    }
}   
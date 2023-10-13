using Microsoft.AspNetCore.Identity;

namespace CityExplorer.Models
{
    public class AppUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfilePicture { get; set; }
    }
}

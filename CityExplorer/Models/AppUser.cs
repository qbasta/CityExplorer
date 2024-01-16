using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CityExplorer.Models;

public class AppUser : IdentityUser
{
    public string       Login          { get; set; } = "Anonymous";
    public string?      FirstName      { get; set; }
    public string?      LastName       { get; set; }
    public string?      ProfilePicture { get; set; }
    public List<Review> Reviews        { get; set; } = new();
}

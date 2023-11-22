using System.ComponentModel.DataAnnotations.Schema;

namespace CityExplorer.Models;

public class LandmarkCategory
{
    [ForeignKey("Landmark")]
    public int LandmarkId { get; set; }
    public required Landmark Landmark { get; set; }

    [ForeignKey("Category")]
    public int CategoryId { get; set; }
    public required Category Category { get; set; }
}

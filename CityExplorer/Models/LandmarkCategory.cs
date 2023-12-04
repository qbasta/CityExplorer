using CityExplorer.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityExplorer.Models;

public class LandmarkCategory : ModelBase
{
    [ForeignKey("Landmark")]
    public int LandmarkId       { get; set; }
    public Landmark Landmark    { get; set; }

    [ForeignKey("Category")]
    public int CategoryId       { get; set; }
    public Category Category    { get; set; }

}

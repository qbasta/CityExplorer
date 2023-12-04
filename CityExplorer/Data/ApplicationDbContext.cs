using CityExplorer.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CityExplorer.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Landmark>         Landmarks          { get; set; }
    public DbSet<City>             Cities             { get; set; }
    public DbSet<Review>           Reviews            { get; set; }
    public DbSet<AppUser>          AppUsers           { get; set; }
    public DbSet<Category>         Categories         { get; set; }
    public DbSet<LandmarkCategory> LandmarkCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<LandmarkCategory>()
            .HasKey(lc => new { lc.LandmarkId, lc.CategoryId });

    }
}
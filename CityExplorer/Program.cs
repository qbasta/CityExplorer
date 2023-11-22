using CityExplorer.Data;
using CityExplorer.Data.DbSeeder;
using CityExplorer.Models;
using CityExplorer.Services.Abstract;
using CityExplorer.Services.Implementation;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddIdentity<AppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddDefaultUI()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider);

builder.Services.AddAuthentication()
    .AddGoogle(googleoptions =>
    {
        googleoptions.ClientId = builder.Configuration.GetSection("GoogleAuthSettings")
            .GetValue<string>("ClientId") ?? throw new NullReferenceException();
        googleoptions.ClientSecret = builder.Configuration.GetSection("GoogleAuthSettings")
            .GetValue<string>("ClientSecret") ?? throw new NullReferenceException();
    })
    .AddFacebook(facebookoptions =>
    {
        facebookoptions.AppId = builder.Configuration.GetSection("FacebookAuthSettings")
            .GetValue<string>("AppId") ?? throw new NullReferenceException();
        facebookoptions.AppSecret = builder.Configuration.GetSection("FacebookAuthSettings")
            .GetValue<string>("AppSecret") ?? throw new NullReferenceException();
    });

builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<ICityService, CityService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

var scopeFactory = app.Services.GetRequiredService<IServiceProvider>();
using (var scope = scopeFactory.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DbSeeder.Seed(context, userManager, roleManager);
}

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();

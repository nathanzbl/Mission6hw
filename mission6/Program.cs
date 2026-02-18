using Microsoft.EntityFrameworkCore;
using mission6.Data;
using mission6.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("MovieConnection")));

var app = builder.Build();

// Migrate + Seed
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();

    if (!db.MovieCategories.Any())
    {
        db.MovieCategories.AddRange(
            new MovieCategory { Name = "Action/Adventure" },
            new MovieCategory { Name = "Comedy" },
            new MovieCategory { Name = "Drama" },
            new MovieCategory { Name = "Family" },
            new MovieCategory { Name = "Horror/Suspense" },
            new MovieCategory { Name = "Television" },
            new MovieCategory { Name = "Miscellaneous" }
        );
        db.SaveChanges();
    }

    if (!db.Movies.Any())
    {
        int actionId = db.MovieCategories.First(c => c.Name == "Action/Adventure").MovieCategoryId;
        int familyId = db.MovieCategories.First(c => c.Name == "Family").MovieCategoryId;
        int comedyId = db.MovieCategories.First(c => c.Name == "Comedy").MovieCategoryId;

        db.Movies.AddRange(
            new Movie
            {
                MovieCategoryId = actionId,
                Title = "The Dark Knight",
                Year = "2008",
                Director = "Christopher Nolan",
                Rating = Rating.PG13,
                Edited = false,
                LentTo = null,
                Notes = "Peak"
            },
            new Movie
            {
                MovieCategoryId = familyId,
                Title = "Toy Story",
                Year = "1995",
                Director = "John Lasseter",
                Rating = Rating.G,
                Edited = null,
                LentTo = null,
                Notes = "Classic"
            },
            new Movie
            {
                MovieCategoryId = comedyId,
                Title = "Back to the Future",
                Year = "1985",
                Director = "Robert Zemeckis",
                Rating = Rating.PG,
                Edited = null,
                LentTo = null,
                Notes = null
            }
        );

        db.SaveChanges();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mission6.Data;
using mission6.Models;

namespace mission6.Controllers;

public class MoviesController : Controller
{
    private readonly ApplicationDbContext _db;

    public MoviesController(ApplicationDbContext db)
    {
        _db = db;
    }

    [HttpGet]
    public IActionResult Index()
    {
        var movies = _db.Movies
            .AsNoTracking()
            .Include(m => m.MovieCategory)
            .OrderBy(m => m.Title)
            .ToList();

        return View(movies);
    }

    [HttpGet]
    public IActionResult Create()
    {
        PopulateCategories();
        PopulateRatings();
        return View(new Movie());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Movie movie)
    {
        if (!ModelState.IsValid)
        {
            PopulateCategories();
            PopulateRatings();
            return View(movie);
        }

        _db.Movies.Add(movie);
        _db.SaveChanges();

        return RedirectToAction(nameof(Index));
    }

    private void PopulateCategories()
    {
        ViewBag.Categories = _db.MovieCategories
            .AsNoTracking()
            .OrderBy(c => c.Name)
            .Select(c => new SelectListItem
            {
                Value = c.MovieCategoryId.ToString(),
                Text = c.Name
            })
            .ToList();
    }

    private void PopulateRatings()
    {
        ViewBag.Ratings = Enum.GetValues(typeof(Rating))
            .Cast<Rating>()
            .Select(r => new SelectListItem
            {
                Value = r.ToString(),
                Text = r == Rating.PG13 ? "PG-13" : r.ToString()
            })
            .ToList();
    }
}
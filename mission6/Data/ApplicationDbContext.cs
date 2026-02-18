namespace mission6.Data;
using Microsoft.EntityFrameworkCore;
using mission6.Models;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Movie> Movies => Set<Movie>();
    public DbSet<MovieCategory> MovieCategories => Set<MovieCategory>();
}
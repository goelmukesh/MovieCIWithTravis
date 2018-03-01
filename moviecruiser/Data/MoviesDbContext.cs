using Microsoft.EntityFrameworkCore;
using moviecruiser.Data.Models;

namespace moviecruiser.Data
{
    public class MoviesDbContext:DbContext, IMoviesDbContext
    {
        public MoviesDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Movie> Movies { get; set; }
    }
}

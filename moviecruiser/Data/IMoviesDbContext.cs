using Microsoft.EntityFrameworkCore;
using moviecruiser.Data.Models;

namespace moviecruiser.Data
{
  public interface IMoviesDbContext
    {
    DbSet<Movie> Movies { get; set; }
    int SaveChanges();
  }
}

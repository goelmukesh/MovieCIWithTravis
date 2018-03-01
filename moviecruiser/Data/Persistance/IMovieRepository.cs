using System.Collections.Generic;
using moviecruiser.Data.Models;

namespace moviecruiser.Data.Persistance
{
  public interface IMovieRepository
  {
    Movie Add(Movie movie);
    List<Movie> GetMovies();
    Movie GetMovieById(int id);
    bool Remove(int id);
    Movie Update(int id, string comment);
  }
}

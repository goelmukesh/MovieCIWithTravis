using System.Collections.Generic;
using System.Linq;
using moviecruiser.Data.Models;
using moviecruiser.Exceptions;

namespace moviecruiser.Data.Persistance
{
  public class MovieRepository : IMovieRepository
  {
    private readonly IMoviesDbContext _context;
    public MovieRepository(IMoviesDbContext context)
    {
      _context = context;
    }

    #region CRUD methods
    public Movie Add(Movie movie)
    {
      Movie _movie = _context.Movies.Find(movie.id);
      if (_movie == null)
      {
        _context.Movies.Add(movie);
        _context.SaveChanges();
        return movie;
      }
      else
      {
        throw new DuplicateMovieFoundException("This movie is already in your favorites");
      }
    }

    public Movie GetMovieById(int id)
    {
      Movie _movie = _context.Movies.Find(id);
      if (_movie != null)
      {
        return _movie;
      }
      else
      {
        throw new MovieNotFoundException("No movie found with this id");
      }
    }

    public List<Movie> GetMovies()
    {
      List<Movie> _movies = _context.Movies.ToList();
      if (_movies.Count > 0)
      {
        return _movies;
      }
      else
      {
        throw new MovieNotFoundException("No movies found");
      }

    }
    public bool Remove(int id)
    {
      Movie _movie = _context.Movies.Find(id);
      if (_movie != null)
      {
        _context.Movies.Remove(_movie);
        _context.SaveChanges();
        return true;
      }
      else
      {
        throw new MovieNotFoundException("No movie found with this id");
      }
    }

    public Movie Update(int id, string comment)
    {
      Movie _movie = _context.Movies.Find(id);
      if (_movie != null)
      {
        _movie.comments = comment;
        _context.SaveChanges();
        return _movie;
      }
      else
      {
        throw new MovieNotFoundException("No movie found with this id");
      }
    }
    #endregion
  }
}

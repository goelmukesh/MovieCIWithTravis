using System;

namespace moviecruiser.Exceptions
{
  public class DuplicateMovieFoundException : ApplicationException
  {
    public DuplicateMovieFoundException() { }
    public DuplicateMovieFoundException(string message) : base(message) { }
  }
}

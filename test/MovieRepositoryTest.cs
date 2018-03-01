using System.Collections.Generic;
using moviecruiser.Data.Models;
using moviecruiser.Data.Persistance;
using Xunit;

namespace test
{
    public class MovieRepositoryTest:IClassFixture<DatabaseFixture>
    {
        DatabaseFixture _fixture;
        private MovieRepository movierepo;
        public MovieRepositoryTest(DatabaseFixture fixture)
        {
            _fixture = fixture;
            movierepo = new MovieRepository(_fixture.dbcontext);
        }

        [Fact]
        public void GetAll_Returns_ListOfMovie()
        {
            //Act
            var actual = movierepo.GetMovies();

            //Assert
            Assert.IsAssignableFrom<List<Movie>>(actual);
            Assert.NotNull(actual);
            Assert.True(actual.Count > 0);
        }

        [Fact]
        public void GetById_Returns_AMovie()
        {
            //Act
            var actual = movierepo.GetMovieById(354441);

            //Assert
            Assert.IsAssignableFrom<Movie>(actual);
            Assert.NotNull(actual);
            Assert.Equal("Anaconda", actual.name);
        }

        [Fact]
        public void AddMovie_Returns_NewMovie()
        {
            //Arrange
            var _movie= new Movie { id = 354443, name = "Lost world", posterPath = "lost.jpg", releaseDate = "05-01-2010", comments = string.Empty, voteAverage = 7.8, voteCount = 980 };

            //Act
            var actual = movierepo.Add(_movie);

            //Assert
            Assert.IsAssignableFrom<Movie>(actual);
        }

        [Fact]
        public void DeleteMovie_Returns_Success()
        {
            //Arrange
            int id = 354440;

            //Act
            var actual = movierepo.Remove(id);

            //Assert
            Assert.True(actual);
        }

        [Fact]
        public void UpdateMovie_Returns_WithComment()
        {
            //Arrange
            int id = 354441;
            string comment = "Everyone must watch this movie";

            //Act
            var actual = movierepo.Update(id, comment);

            //Assert
            Assert.IsAssignableFrom<Movie>(actual);
            Assert.Equal(comment, actual.comments);
        }
    }
}

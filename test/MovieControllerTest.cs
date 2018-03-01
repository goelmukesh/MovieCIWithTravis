using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using moviecruiser.Controllers;
using moviecruiser.Data.Models;
using moviecruiser.Data.Persistance;
using moviecruiser.ViewModels;
using moviecruiser.Exceptions;
using Xunit;

namespace test
{
    public class MovieControllerTest
    {
        [Fact]
        public void Get_Returns_AListOfMovies()
        {
            //Arrange
            var mockrepo = new Mock<IMovieRepository>();
            mockrepo.Setup(repo=>repo.GetMovies()).Returns(GetMovies());
            var controller = new MovieController(mockrepo.Object);

            //Act
            var result = controller.Get();

            //Assert
            var actionresult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<ApiResponse>(actionresult.Value);
            Assert.True(model.Success);
            Assert.IsAssignableFrom<IEnumerable<Movie>>(model.Data);
        }

        [Fact]
        public void Get_Returns_Null()
        {
            //Arrange
            var mockrepo = new Mock<IMovieRepository>();
            mockrepo.Setup(repo => repo.GetMovies()).Throws<MovieNotFoundException>(); 
            var controller = new MovieController(mockrepo.Object);

            //Act
            var result = controller.Get();

            //Assert
            var actionresult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<ApiResponse>(actionresult.Value);
            Assert.False(model.Success);
            Assert.Null(model.Data);
        }

        [Fact]
        public void GetById_Returns_Success()
        {
            //Arrange
            var mockrepo = new Mock<IMovieRepository>();
            var _movie = new Movie { id = 354440, name = "Superman", posterPath = "superman.jpg", releaseDate = "12-10-2012", comments = string.Empty, voteAverage = 7.8, voteCount = 980 };
            mockrepo.Setup(repo => repo.GetMovieById(354440)).Returns(_movie); 
            var controller = new MovieController(mockrepo.Object);

            //Act
            var result = controller.Get(354440);

            //Assert
            var actionresult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<ApiResponse>(actionresult.Value);
            Assert.True(model.Success);
            Assert.IsAssignableFrom<Movie>(model.Data);
        }

        [Fact]
        public void Post_Returns_Success()
        {
            //Arrange
            var mockrepo = new Mock<IMovieRepository>();
            var _movie = new Movie { id = 354440, name = "Superman", posterPath = "superman.jpg", releaseDate = "12-10-2012", comments = string.Empty, voteAverage = 7.8, voteCount = 980 };
            mockrepo.Setup(repo => repo.Add(_movie)).Returns(_movie);
            var controller = new MovieController(mockrepo.Object);

            //Act
            var result = controller.Post(_movie);

            //Assert
            var actionresult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(201, actionresult.StatusCode);
            var model = Assert.IsAssignableFrom<ApiResponse>(actionresult.Value);
            Assert.True(model.Success);
        }

        [Fact]
        public void Delete_Returns_Success()
        {
            //Arrange
            var mockrepo = new Mock<IMovieRepository>();
            mockrepo.Setup(repo => repo.Remove(354440));
            var controller = new MovieController(mockrepo.Object);

            //Act
            var result = controller.Delete(354440);

            //Assert
            var actionresult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(202, actionresult.StatusCode);
            var model = Assert.IsAssignableFrom<ApiResponse>(actionresult.Value);
            Assert.True(model.Success);
        }


        private List<Movie> GetMovies()
        {
            var movie = new List<Movie>();
            movie.Add(new Movie { id=354440, name="Superman", posterPath="superman.jpg", releaseDate="12-10-2012", comments=string.Empty, voteAverage=7.8, voteCount=980 });
            return movie;
        }
    }
}

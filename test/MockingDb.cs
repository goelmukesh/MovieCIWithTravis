using System;
using System.Collections.Generic;
using System.Linq;
using moviecruiser.Data;
using moviecruiser.Data.Models;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace test
{
    public class DatabaseFixture : IDisposable
    {
        private IEnumerable<Movie> Movies { get; set; }
        public IMoviesDbContext dbcontext;
        public DatabaseFixture()
        {
            var options = new DbContextOptionsBuilder<MoviesDbContext>()
                .UseInMemoryDatabase(databaseName: "MovieDB")
                .Options;
            dbcontext = new MoviesDbContext(options);

            // Insert seed data into the database using one instance of the context
            dbcontext.Movies.Add(new Movie { id = 354440, name = "Superman", posterPath = "superman.jpg", releaseDate = "12-10-2012", comments = string.Empty, voteAverage = 7.8, voteCount = 980 });
            dbcontext.Movies.Add(new Movie { id = 354441, name = "Anaconda", posterPath = "anaconda.jpg", releaseDate = "12-10-2012", comments = string.Empty, voteAverage = 8.0, voteCount = 1080 });
            dbcontext.Movies.Add(new Movie { id = 354442, name = "Independence Day", posterPath = "spiderman.jpg", releaseDate = "12-10-2012", comments = string.Empty, voteAverage = 7.8, voteCount = 980 });
            dbcontext.SaveChanges();
        }
        public void Dispose()
        {
            Movies = null;
            dbcontext = null;
        }
    }
    
    public static class DbSetExtensions
    {
        public static DbSet<T> GetQueryableMockDbSet<T>(this IEnumerable<T> sourceList) where T : class
        {
            var queryable = sourceList.AsQueryable();

            var dbSet = new Mock<DbSet<T>>();
            dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());

            return dbSet.Object;
        }
    }
}

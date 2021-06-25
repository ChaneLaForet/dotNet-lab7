using Lab2.Data;
using Lab2.Services;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace Tests
{
    public class Test_MovieService
    {
        private ApplicationDbContext _context;

        [SetUp]
        public void Setup()
        {
            Console.WriteLine("In setup.");

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                            .UseInMemoryDatabase(databaseName: "TestDB")
                            .Options;

            _context = new ApplicationDbContext(options, new OperationalStoreOptionsForTests());

            _context.Movies.Add(new Lab2.Models.Movie { Id = 100, Title = "The Shawshank Redemption", Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.", 
                Genre = "Drama", DurationInMinutes = 152, YearOfRelease = 1992, 
                Director = "Frank Darabont", DateAdded = Convert.ToDateTime("2016-12-15T09:30:00"), 
                Rating = 9.3F, Watched = true });
            _context.Movies.Add(new Lab2.Models.Movie { Title = "The Godfather", Description = "An organized crime dynasty's aging patriarch transfers control of his clandestine empire to his reluctant son.", 
                Genre = "Crime", DurationInMinutes = 175, YearOfRelease = 1972, 
                Director = "Francis Ford Coppola", DateAdded = Convert.ToDateTime("2020-10-13T03:15:00"), 
                Rating = 9.2F, Watched = false });

            _context.SaveChanges();
        }

        [TearDown]
        public void Teardown()
        {
            Console.WriteLine("In teardown");

            foreach (var movie in _context.Movies)
            {
                _context.Remove(movie);
            }
            _context.SaveChanges();
        }

        /*
        [Test]
        public void Test_GetAll()
        {
            var service = new MovieManagementService(_context);
            Assert.AreEqual(2, service.GetMovies().ResponseOk.Count);
        }
        */

        /*
        [Test]
        public void Test_GetById()
        {
            var service = new MovieManagementService(_context);
            Assert.AreEqual(1, service.GetMovie(100).ResponseOk.Count);
            Assert.AreEqual(0, service.GetMovie(99).ResponseOk.Count);
        }
        */

        [Test]
        public void Test_MovieExists()
        {
            var service = new MovieManagementService(_context);
            Assert.True(service.MovieExists(100));
            Assert.False(service.MovieExists(99));
        }
    }
}
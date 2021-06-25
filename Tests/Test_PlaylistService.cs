using Lab2.Data;
using Lab2.Models;
using Lab2.Services;
using Lab2.ViewModels.Playlists;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests
{
    class Test_PlaylistService
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

            _context.Playlists.Add(new Lab2.Models.Playlist
            {
                Id = 100,
                PlaylistName = "p1",
                Movies = new List<Movie>(),
                ApplicationUser = new ApplicationUser { Id = "100" },
                PlaylistDateTime = Convert.ToDateTime("2016-12-15T09:30:00")
            });
            _context.Playlists.Add(new Lab2.Models.Playlist
            {
                Id = 111,
                PlaylistName = "p2",
                Movies = new List<Movie>(),
                ApplicationUser = new ApplicationUser { Id = "111" },
                PlaylistDateTime = Convert.ToDateTime("2020-09-20T11:25:00")
            });

            _context.SaveChanges();
        }

        [TearDown]
        public void Teardown()
        {
            Console.WriteLine("In teardown");

            foreach (var playlist in _context.Playlists)
            {
                _context.Remove(playlist);
            }
            _context.SaveChanges();
        }

        /*
        [Test]
        public void Test_GetAll()
        {
            var service = new PlaylistManagementService(_context);
            Assert.AreEqual(1, service.GetAll("100").ResponseOk.Count);
        }
        */

        /*
        [Test]
        public void Test_GetPlaylistById()
        {
            var service = new PlaylistManagementService(_context);
            Assert.AreEqual(1, service.GetPlaylistById("100", 100).ResponseOk.Count);
            Assert.AreEqual(0, service.GetPlaylistById("99", 1).ResponseOk.Count);
        }
        */

        [Test]
        public void Test_PlaylistExists()
        {
            var service = new PlaylistManagementService(_context);
            Assert.True(service.PlaylistExists(100));
            Assert.False(service.PlaylistExists(99));
        }

        /*
        [Test]
        public void Test_UpdatePlaylist()
        {
            var service = new PlaylistManagementService(_context);
            var playlist = service.GetPlaylistById("100", 100).ResponseOk;
            var updatedPlaylistViewModel = new UpdatedPlaylistViewModel { PlaylistName = "new name", MovieIds = new List<int>()};

            var serviceResponse = service.UpdatePlaylist(playlist, updatedPlaylistViewModel);

            Assert.False(service.PlaylistExists(100));
        }
        */
    }
}

using Lab2.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Data
{
    public class SeedPlaylists
    {
        private static string Characters = "abcdefghijklmnopqrstuvwxyz123456890";
        private static Random random = new Random();

        public static void Seed(IServiceProvider serviceProvider, int count)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();

            List<Movie> getMovies(List<int> ids)
            {
                int numberOfElements = getNumber(1, 5);
                List<Movie> movies = new();

                for (int i = 1; i <= numberOfElements; i++)
                {
                    int index = random.Next(ids.Count);
                    movies.Add(context.Movies.Find(ids[index]));
                }
                return movies;
            }

            ApplicationUser getUser(List<string> ids)
            {
                ApplicationUser user = new();

                int index = random.Next(ids.Count);
                user = context.ApplicationUsers.Find(ids[index]);

            return user;
            }

            context.Database.EnsureCreated();

            if (context.Playlists.Count() < 20)
            {
                var userIds = context.ApplicationUsers.Select(u => u.Id).ToList();
                var movieIds = context.Movies.Select(m => m.Id).ToList();
                var playlists = new List<Playlist>();

                for (int i = 0; i < count; ++i)
                {
                    var playlist = new Models.Playlist
                    {
                        PlaylistName = getString(1, 50),
                        ApplicationUser = getUser(userIds),
                        Movies = getMovies(movieIds),
                        PlaylistDateTime = getDate()
                    };

                    context.Playlists.Add(playlist);
                }
                context.SaveChanges();
            }
        }

        private static string getString(int min, int max)
        {
            string randomString = "";
            for (int j = 0; j < random.Next(min, max); ++j)
            {
                randomString += Characters[random.Next(Characters.Length)];
            }

            return randomString;
        }

        private static int getNumber(int min, int max)
        {
            int randomNumber = random.Next(min, max);

            return randomNumber;
        }

        private static DateTime getDate()
        {
            DateTime start = new DateTime(2000, 1, 1);
            int range = (DateTime.Today - start).Days;

            return start.AddDays(random.Next(range));
        }

        private static bool getBoolean()
        {
            if (random.NextDouble() >= 0.5)
                return true;
            return false;
        }
    }
}


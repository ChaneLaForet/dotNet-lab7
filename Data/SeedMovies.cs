using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Data
{
    public class SeedMovies
    {
        private static string Characters = "abcdefghijklmnopqrstuvwxyz123456890";
        private static Random random = new Random();

        public static void Seed(IServiceProvider serviceProvider, int count)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();

            if (context.Movies.Count() < 20)
            {
                for (int i = 0; i < count; ++i)
                {

                    context.Movies.Add(new Models.Movie
                    {
                        Title = getString(1,50),
                        Description = getString(20, 200),
                        Genre = getGenre(),
                        DurationInMinutes = getNumber(1, 1500),
                        YearOfRelease = getNumber(1800, DateTime.Now.Year),
                        Director = getString(1, 30),
                        DateAdded = getDate()
                    });
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

        private static string getGenre()
        {
            List<string> genreList = new List<string>(new string[] { "action", "comedy", "horror", "thriller" });
            int index = random.Next(genreList.Count);

            return genreList[index];
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
    }
}
using Lab2.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Data
{
    public class SeedComments
    {
        private static string Characters = "abcdefghijklmnopqrstuvwxyz123456890";
        private static Random random = new Random();

        public static void Seed(IServiceProvider serviceProvider, int count)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();

            Movie getMovie(List<int> ids)
            {
                Movie movie = new();

                int index = random.Next(ids.Count);
                movie = context.Movies.Find(ids[index]);

                return movie;
            }

            if (context.Comments.Count() < 20)
            {
                var movieIds = context.Movies.Select(m => m.Id).ToList();

                for (int i = 0; i < count; ++i)
                {
                    var comment = new Comment
                    {
                        Content = getString(1,150),
                        DateTime = getDate(),
                        Rating = getNumber(1,10),
                        Movie = getMovie(movieIds)
                    };
                    context.Comments.Add(comment);
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
    }
}

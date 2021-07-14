using Lab2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lab2.Data
{
    public class SeedUsers
    {
        private static string Characters = "abcdefghijklmnopqrstuvwxyz123456890";
        private static Random random = new Random();

        public static void Seed(IServiceProvider serviceProvider, int count)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();

            if (context.ApplicationUsers.Count() < 20)
            {

                for (int i = 0; i < count; ++i)
                {
                    var email = Faker.Internet.Email();
                    var username = Faker.Internet.UserName() + i; //TODO: find other ways to keep usernames unique so a user can be added
                    //for example, save every username faker used in a list and check a new one against it... but search on the internet first

                    var user = new ApplicationUser
                    {
                        Email = email,
                        NormalizedEmail = email.ToUpper(),
                        UserName = username,
                        NormalizedUserName = username.ToUpper(),
                        SecurityStamp = Guid.NewGuid().ToString(),
                        EmailConfirmed = true,
                    };

                    user.PasswordHash = getHashedPassword(user);
                    context.ApplicationUsers.Add(user);
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

        private static string getEmail()
        {
            string email = getString(3, 5) + "@" + getString(3, 5) + "." + getString(2,3);
            return email;
        }

        private static string getHashedPassword(ApplicationUser user)
        {
            var password = new PasswordHasher<ApplicationUser>();
            var hashed = password.HashPassword(user, "passwordPASS123!@#");
            return hashed;
        }
    }
}

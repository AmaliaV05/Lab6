using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Project2.Models;

namespace Project2.Data
{
    public static class SeedFilms
    {
        private static string Characters = "abcdefghijklmnopqrstuvwxyz123456890";

        public static void Seed(IServiceProvider serviceProvider, int count)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();

            Random random = new Random();

            if (context.Films.Count() < 10)
            {
                for (int i = 0; i < count; ++i)
                {
                    string title = "";
                    string description = "";
                    Genre genre = 0;
                    string duration = "";
                    int yearOfRelease = 0;
                    string director = "";
                    DateTime dateAdded = new DateTime();
                    int rating = 0;
                    string watched = "";
                    for (int j = 0; j < random.Next(3, 10); ++j)
                    {
                        title += Characters[random.Next(Characters.Length)];
                        duration += Characters[random.Next(Characters.Length)];
                        director += Characters[random.Next(Characters.Length)];
                    }
                    for (int j = 0; j < random.Next(2000, 2020); ++j)
                    {
                        yearOfRelease += random.Next();
                    }
                    for (int j = 0; j < random.Next(0, 3); ++j)
                    {
                        genre += random.Next();
                    }
                    for (int j = 0; j < random.Next(21, 100); ++j)
                    {
                        description += Characters[random.Next(Characters.Length)];
                    }
                    context.Films.Add(new Film
                    {
                        Title = title,
                        Description = description,
                        Genre = genre
                        //Price = random.NextDouble() * random.Next(200, 5000)
                    });
                }

                context.SaveChanges();
            }
        }
    }
}

using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Project2.Models;

namespace Project2.Data
{
    public static class SeedFilms
    {
        private static readonly string Characters = "abcdefghijklmnopqrstuvwxyz123456890 ";

        public static void Seed(IServiceProvider serviceProvider, int count)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();

            Random random = new();

            if (context.Films.Count() < 10)
            {
                for (int i = 0; i < count; ++i)
                {
                    string title = "";
                    string description = "";
                    string duration = "";
                    string director = "";
                    string [] watched = { "yes", "no"};
                    for (int j = 0; j < random.Next(10, 20); ++j)
                    {
                        title += Characters[random.Next(Characters.Length)];
                        duration += Characters[random.Next(Characters.Length)];
                        director += Characters[random.Next(Characters.Length)];
                    }
                    int yearOfRelease = random.Next(2000, 2020);
                    
                    Genre genre = (Genre)random.Next(0, 4);

                    for (int j = 0; j < random.Next(21, 100); ++j)
                    {
                        description += Characters[random.Next(Characters.Length)];
                    }
                    Random gen = new Random();
                    int range = 5 * 365; //5 years         
                    
                    
                    context.Films.Add(new Film
                    {
                        Title = title,
                        Description = description,
                        Genre = genre,
                        Duration = duration,
                        YearOfRelease = yearOfRelease,
                        Director = director,
                        DateAdded = DateTime.Today.AddDays(-gen.Next(range)),
                        Rating = random.Next(1, 11),                        
                        Watched = watched[random.Next(watched.Length)]
                });
                }

                context.SaveChanges();
            }
        }
    }
}

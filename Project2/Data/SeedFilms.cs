using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

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
                    for (int j = 0; j < random.Next(3, 10); ++j)
                    {
                        title += Characters[random.Next(Characters.Length)];
                        description += Characters[random.Next(Characters.Length)];
                    }
                    context.Films.Add(new Models.Film
                    {
                        Title = title
                        //Price = random.NextDouble() * random.Next(200, 5000)
                    });
                }

                context.SaveChanges();
            }
        }
    }
}

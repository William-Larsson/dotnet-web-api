using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using web_api.Models;

namespace web_api.Data
{
    public class AppDBInitializer
    {
        public static void Seed (IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDBContext>();

                // If database is empty, seed it with initial data. 
                if (!context.Songs.Any())
                {
                    context.Songs.AddRange(
                    new Song {
                        Title = "Back In Black",
                        ReleaseDate = new System.DateTime(1980, 7, 25),
                        Genre = "Hard Rock"
                    },
                    new Song {
                        Title = "Wow.",
                        ReleaseDate = new System.DateTime(2018, 12, 24),
                        Genre = "Hip Hop"
                    },
                    new Song {
                        Title = "You Make Me",
                        ReleaseDate = new System.DateTime(2013, 8, 30),
                        Genre = "EDM"
                    });

                    context.SaveChanges();
                }
            }
        }
    }
}
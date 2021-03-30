using Microsoft.EntityFrameworkCore;
using web_api.Models;

namespace web_api.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext (DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        public DbSet<Song> Song { get; set; }
    }
}
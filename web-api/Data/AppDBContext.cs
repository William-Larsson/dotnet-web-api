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

        // Added in order to allow for the Many-to-Many relation between
        // Songs and Artist. 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Defines relation between Songs and Song_Artist tables. 
            modelBuilder.Entity<Song_Artist>()
                .HasOne(s => s.Song)
                .WithMany(sa => sa.Song_Artists)
                .HasForeignKey(si => si.SongId);

            // Defines relation between Artist and Song_Artist tables. 
            modelBuilder.Entity<Song_Artist>()
                .HasOne(s => s.Artist)
                .WithMany(sa => sa.Song_Artists)
                .HasForeignKey(si => si.ArtistId);
        }


        // DbSets are used to be able to access all the data from 
        // the database tables. With this, data can't be added or
        // sent to each table.
        public DbSet<Song> Songs { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song_Artist> Songs_Artists { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

    }
}
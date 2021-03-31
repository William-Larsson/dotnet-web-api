namespace web_api.Models
{

    // This a join-model between Song and Artist, 
    // which is used to define the many-to-many 
    // relationship between the tables. 
    public class Song_Artist
    {
        public int Id { get; set; }

        // Navigation props.
        // Used to define the relationship between models.
        public int SongId { get; set; }
        public Song Song { get; set; }

        public int ArtistId { get; set; }
        public Artist Artist { get; set; }


    }
}
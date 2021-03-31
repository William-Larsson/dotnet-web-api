using System.Collections.Generic;

namespace web_api.Models
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation props.
        // Used to define the relationship between models.
        // Many-to-Many between Artist and Song
        public List<Song_Artist> Song_Artists { get; set; }
    }
}
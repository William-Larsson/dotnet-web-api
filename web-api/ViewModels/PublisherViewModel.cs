using System.Collections.Generic;

namespace web_api.Models
{
    public class PublisherViewModel
    {
        public string Name { get; set; }
    }

    public class PublisherWithSongsAndArtists
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SongArtistViewModel> SongArtists { get; set; }
    }

    public class SongArtistViewModel
    {
        public int Id { get; set; }
        public string SongName { get; set; }
        public List<string> SongArtists { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace web_api.Models
{
    // User exposed version of the Song.cs class.
    // This should not be used as the DB-entity, 
    // but instead is meant as a middlelayer to 
    // make the CRUD operations easier and less crowded. 
    public class SongViewModel
    {
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }

        // Will be mapped to Navigation props in the real Entities.
        public int? PublisherId { get; set; } 
        public List<int> ArtistsIds { get; set; }

    }

     public class SongWithArtists
    {
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }

        // Will be mapped to Navigation props in the real Entities.
        public string PublisherName { get; set; } 
        public List<string> ArtistNames { get; set; }

    }
}
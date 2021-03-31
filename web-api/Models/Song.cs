using System;
using System.ComponentModel.DataAnnotations;

namespace web_api.Models
{
    public class Song
    {
        public int Id { get; set; }
        public string Title { get; set; }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }

        // Navigation props.
        // Used to define the relationship between models.E
        // A song as a single publisher
        public int? PublisherId { get; set; }  // Foreign key (optional, songs can have no publisher)
        public Publisher Publisher { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace consumer_ui.Models
{
    public class SongDetail
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "* Required")]
        public string Title { get; set; }

        [DisplayName("Release date")]
        [Required(ErrorMessage = "* Required")]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "* Required")]
        public string Genre { get; set; }

        [DisplayName("Publisher name")]
        public string PublisherName { get; set; }

        [DisplayName("Publisher ID")]
        [Required(ErrorMessage = "* Required")]
        public int PublisherId { get; set; }

        [DisplayName("Artists")]
        public List<string> ArtistNames { get; set; }

        [DisplayName("Artists ID's")]
        [Required(ErrorMessage = "* Required")]
        public List<int> ArtistIds { get; set; }
        
    }
}
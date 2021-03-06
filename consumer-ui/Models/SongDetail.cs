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
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }

        [Required(ErrorMessage = "* Required")]
        public string Genre { get; set; }

        [DisplayName("Publisher name")]
        public string PublisherName { get; set; }

        // NOTE! [Require] must still be complemented 
        // with an API check to see if the publisherId
        // is a real publisher.
        [DisplayName("Publisher ID")]
        [Required(ErrorMessage = "* Required")]
        public int PublisherId { get; set; }

        [DisplayName("Artists")]
        public List<string> ArtistNames { get; set; }

        // NOTE! Cannot automatically validate this input
        // with [Required]m need to be implemented in the 
        // controller. 
        [DisplayName("Artists ID's")]
        public List<int> ArtistIds { get; set; }
        
    }
}
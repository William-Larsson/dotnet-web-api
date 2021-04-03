using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace consumer_ui.Models
{

    // Class based on attributes given from the API-Get from /api/artist
    public class ArtistDetail
    {

        [DisplayName("Artist ID")]
        public int Id { get; set; }


        [DisplayName("Artist Name")]
        [Required(ErrorMessage = "* Required")]
        public string Name { get; set; }


        [Required(ErrorMessage = "* Required")]
        public List<string> Songs { get; set; }


        // TODO: I need a list song IDs too, right?
    }
}
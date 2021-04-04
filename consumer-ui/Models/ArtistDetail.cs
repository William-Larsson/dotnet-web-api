using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace consumer_ui.Models
{

    // Class based on attributes given from the API-Get from /api/artist
    public class ArtistDetail
    {
        [DisplayName("Artist ID")]
        public int Id { get; set; }

        [DisplayName("Artist Name")]
        [StringLength(50, ErrorMessage = 
            "Record Label name must be between 1 and 50 characters.")]
        [BindRequired]
        public string Name { get; set; }

        [DisplayName("Songs by artist")]
        public List<string> SongTitles { get; set; }
    }
}
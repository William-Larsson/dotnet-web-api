using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace consumer_ui.Models
{
    public class PublisherDetail
    {
        [DisplayName("Publisher ID")]
        public int Id { get; set; }

        [DisplayName("Publisher name")]
        [StringLength(50, ErrorMessage = 
            "Record Label name must be between 1 and 50 characters.")]
        [BindRequired]
        public string Name { get; set; }

        // TODO: add this?
        //public List<SongArtistDetail> SongArtist { get; set; }
    }
}
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace consumer_ui.Models
{
    public class PublisherDetail
    {
        [DisplayName("Publisher ID")]
        public int Id { get; set; }

        [DisplayName("Publisher name")]
        [Required(ErrorMessage = "* Required")]
        public string Name { get; set; }

        // TODO: add this?
        //public List<SongArtistDetail> SongArtist { get; set; }
    }
}
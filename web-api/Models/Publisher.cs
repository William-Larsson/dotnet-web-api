using System.Collections.Generic;

namespace web_api.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Navigation props.
        // Used to define the relationship between models.
        // One publisher <---> Many Songs
        public List<Song> Songs { get; set; }
    }
}
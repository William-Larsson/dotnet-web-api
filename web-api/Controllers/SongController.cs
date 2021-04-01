using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api.Data;
using web_api.Models;

namespace web_api.Controllers
{
    // Used to receive HTTP Requests and to respond to the
    // request with an HTTP Response. 
    [Route("api/[controller]")] // The URI to the controller
    [ApiController]
    public class SongController : ControllerBase
    {
        private readonly AppDBContext _DBContext;

        public SongController(AppDBContext context)
        {
            // Dependency injection of DB context
            _DBContext = context;
        }

        // GET: api/Song
        // Returns all the songs from the DB by HTTP Get.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SongWithArtists>>> GetSong()
        {
            var _songs = await _DBContext.Songs.ToListAsync();
            var _songsWithArtists = new List<SongWithArtists>();

            foreach (var song in _songs)
            {
                _songsWithArtists.Add(await _DBContext.Songs
                        .Where(n => n.Id == song.Id).Select(song => new SongWithArtists()
                    {
                        Id = song.Id,
                        Title = song.Title,
                        ReleaseDate = song.ReleaseDate,
                        Genre = song.Genre,
                        PublisherName = song.Publisher.Name,
                        ArtistNames = song.Song_Artists.Select(n => n.Artist.Name).ToList()
                    }).FirstOrDefaultAsync()
                );
            }

            return _songsWithArtists;
        }

        // GET: api/Song/5
        // Returns a single song by the given id by HTTP Get.  
        [HttpGet("{id}")]
        public async Task<ActionResult<SongWithArtists>> GetSong(int id)
        {
            var _songWithArtist = await _DBContext.Songs
                .Where(n => n.Id == id).Select(song => new SongWithArtists()
            {
                Id = song.Id,
                Title = song.Title,
                ReleaseDate = song.ReleaseDate,
                Genre = song.Genre,
                PublisherName = song.Publisher.Name,
                ArtistNames = song.Song_Artists.Select(n => n.Artist.Name).ToList()
            }).FirstOrDefaultAsync();

            return _songWithArtist;
        }

        // PUT: api/Song/5
        // Updates an existing book in the DB if exists. 
        // The URI Id must match the Song-object Id. 
        [HttpPut("{id}")]
        public Task<IActionResult> PutSong(int id, [FromBody] SongViewModel songVM)
        {
            // If I figure out how to implement this method, add "async" to declaration.
            throw new NotImplementedException();
        }

        // POST: api/Song
        // Will add a new song to the database. 
        // When creating a Song-object, the Id prop should be set 
        // to 0 or left as null. 
        [HttpPost]
        public async Task<ActionResult<Song>> PostSong([FromBody] SongViewModel songVM)
        {
            // Add the song to the Song-table with data from songVM
            var _song = new Song()
            {
                Title = songVM.Title,
                ReleaseDate = songVM.ReleaseDate,
                Genre = songVM.Genre,
                PublisherId = songVM.PublisherId
            };

            _DBContext.Songs.Add(_song); 
            await _DBContext.SaveChangesAsync(); 

            // After saving the new song, add all artists-relations
            // for the song into the Artists-table. 
            foreach(var id in songVM.ArtistsIds)
            {
                var _song_artist = new Song_Artist()
                {
                    SongId = _song.Id,
                    ArtistId = id
                };

                _DBContext.Songs_Artists.Add(_song_artist); 
                await _DBContext.SaveChangesAsync(); 
            }

            // HTTP Response?
            return CreatedAtAction("GetSong", new { id = _song.Id }, _song);
        }

        //DELETE: api/Song/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            var song = await _DBContext.Songs.FindAsync(id);
            if (song == null)
            {
                return NotFound();
            }

            _DBContext.Songs.Remove(song);
            await _DBContext.SaveChangesAsync();

            return NoContent();
        }

        private bool SongExists(int id)
        {
            return _DBContext.Songs.Any(e => e.Id == id);
        }
    }
}

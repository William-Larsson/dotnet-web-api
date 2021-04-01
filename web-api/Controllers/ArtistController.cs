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
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistController : ControllerBase
    {
        private readonly AppDBContext _DBContext;

        public ArtistController(AppDBContext context)
        {
            _DBContext = context;
        }

        // GET: api/Artist
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArtistWithSongs>>> GetArtists()
        {
            var _artists = await _DBContext.Artists.ToListAsync();
            var _artistWithSongs = new List<ArtistWithSongs>();

            foreach (var artist in _artists)
            {
                _artistWithSongs.Add(await _DBContext.Artists
                    .Where(n => n.Id == artist.Id).Select(artist => new ArtistWithSongs()
                    {
                        Id = artist.Id,
                        Name = artist.Name,
                        SongTitles = artist.Song_Artists.Select(n => n.Song.Title).ToList()
                    }).FirstOrDefaultAsync()
                );
            }

            return _artistWithSongs;
        }

        // GET: api/Artist/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistWithSongs>> GetArtist(int id)
        {
            var _artist = await _DBContext.Artists
                .Where(n => n.Id == id).Select(artist => new ArtistWithSongs()
                {
                    Id = artist.Id,
                    Name = artist.Name,
                    SongTitles = artist.Song_Artists.Select(n => n.Song.Title).ToList()
                }).FirstOrDefaultAsync();

            return _artist;
        }

        // PUT: api/Artist/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArtist(int id, ArtistViewModel artistVM)
        {
            var _artist = new Artist(){
                Id = id,
                Name = artistVM.Name
            };

            _DBContext.Entry(_artist).State = EntityState.Modified;

            try
            {
                await _DBContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArtistExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Artist
        [HttpPost]
        public async Task<ActionResult<Artist>> PostArtist([FromBody] ArtistViewModel artistVM)
        {
            var _artist = new Artist(){
                Name = artistVM.Name
            };

            _DBContext.Artists.Add(_artist);
            await _DBContext.SaveChangesAsync();

            return CreatedAtAction("GetArtist", new { id = _artist.Id }, _artist);
        }

        //DELETE: api/Artist/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            var artist = await _DBContext.Artists.FindAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            _DBContext.Artists.Remove(artist);
            await _DBContext.SaveChangesAsync();

            return NoContent();
        }

        private bool ArtistExists(int id)
        {
            return _DBContext.Artists.Any(e => e.Id == id);
        }
    }
}

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
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<Artist>>> GetArtists()
        // {
        //     // TODO: does not return the publisher name and artist names
        //     return await _DBContext.Artists.ToListAsync();
        // }

        // GET: api/Artist/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ArtistWithSongs>> GetArtist(int id)
        {
            var _artist = await _DBContext.Artists
                .Where(n => n.Id == id).Select(artist => new ArtistWithSongs()
                {
                    Name = artist.Name,
                    SongTitles = artist.Song_Artists.Select(n => n.Song.Title).ToList()
                }).FirstOrDefaultAsync();

            return _artist;
        }

        // PUT: api/Artist/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutArtist(int id, Artist artist)
        // {
        //     if (id != artist.Id)
        //     {
        //         return BadRequest();
        //     }

        //     _DBContext.Entry(artist).State = EntityState.Modified;

        //     try
        //     {
        //         await _DBContext.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!ArtistExists(id))
        //         {
        //             return NotFound();
        //         }
        //         else
        //         {
        //             throw;
        //         }
        //     }

        //     return NoContent();
        // }

        // POST: api/Artist
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

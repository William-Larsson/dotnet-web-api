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
        public async Task<ActionResult<IEnumerable<Song>>> GetSong()
        {
            return await _DBContext.Songs.ToListAsync();
        }

        // GET: api/Song/5
        // Returns a single song by the given Id by HTTP Get.  
        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSong(int id)
        {
            var song = await _DBContext.Songs.FindAsync(id);

            if (song == null)
            {
                return NotFound();
            }

            return song;
        }

        // PUT: api/Song/5
        // Updates an existing book in the DB if exists. 
        // The URI Id must match the Song-object Id. 
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSong(int id, Song song)
        {
            if (id != song.Id)
            {
                return BadRequest();
            }

            _DBContext.Entry(song).State = EntityState.Modified;

            try
            {
                await _DBContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SongExists(id))
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

        // POST: api/Song
        // Will add a new song to the database. 
        // When creating a Song-object, the Id prop should be set 
        // to 0 or left as null. 
        [HttpPost]
        public async Task<ActionResult<Song>> PostSong(Song song)
        {
            _DBContext.Songs.Add(song); 
            await _DBContext.SaveChangesAsync(); 

            // HTTP Response?
            return CreatedAtAction("GetSong", new { id = song.Id }, song);
        }

        // DELETE: api/Song/5
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

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
    public class PublisherController : ControllerBase
    {
        private readonly AppDBContext _DBContext;

        public PublisherController(AppDBContext context)
        {
            _DBContext = context;
        }

        // GET: api/Publisher
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<Publisher>>> GetPublishers()
        // {
        //     return await _DBContext.Publishers.ToListAsync();
        // }

        // GET: api/Publisher/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublisherWithSongsAndArtists>> GetPublisher(int id)
        {
            var _publisherData = await _DBContext.Publishers
                .Where(p => p.Id == id).Select(pub => new PublisherWithSongsAndArtists()
                {
                    Name = pub.Name,
                    SongArtists = pub.Songs.Select(song => new SongArtistViewModel()
                    {
                        SongName = song.Title,
                        SongArtists = song.Song_Artists.Select(sa => sa.Artist.Name).ToList()
                    }).ToList()
                }).FirstOrDefaultAsync();

            return _publisherData;
        }

        // PUT: api/Publisher/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPut("{id}")]
        // public async Task<IActionResult> PutPublisher(int id, Publisher publisher)
        // {
        //     if (id != publisher.Id)
        //     {
        //         return BadRequest();
        //     }

        //     _DBContext.Entry(publisher).State = EntityState.Modified;

        //     try
        //     {
        //         await _DBContext.SaveChangesAsync();
        //     }
        //     catch (DbUpdateConcurrencyException)
        //     {
        //         if (!PublisherExists(id))
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

        // POST: api/Publisher
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Publisher>> PostPublisher([FromBody] PublisherViewModel publisherVM)
        {
            var _publisher = new Publisher()
            {
                Name = publisherVM.Name
            };

            _DBContext.Publishers.Add(_publisher);
            await _DBContext.SaveChangesAsync();

            return CreatedAtAction("GetPublisher", new { id = _publisher.Id }, _publisher);
        }

        // DELETE: api/Publisher/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePublisher(int id)
        {
            var publisher = await _DBContext.Publishers.FindAsync(id);
            if (publisher == null)
            {
                return NotFound();
            }

            _DBContext.Publishers.Remove(publisher);
            await _DBContext.SaveChangesAsync();

            return NoContent();

            // From tutorial, I think the scaffolded code is fine as is
            // var _publisher = await _DBContext.Publishers
            //     .FirstOrDefaultAsync(pub => pub.Id == id);

            // if (_publisher != null)
            // {
            //     _DBContext.Remove(_publisher);
            //     await _DBContext.SaveChangesAsync();
            // }

            // return NoContent();
        }

        private bool PublisherExists(int id)
        {
            return _DBContext.Publishers.Any(e => e.Id == id);
        }
    }
}

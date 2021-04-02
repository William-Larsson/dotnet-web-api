using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api.Data;
using web_api.Models;

namespace web_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Only authorized users can access
    public class PublisherController : ControllerBase
    {
        private readonly AppDBContext _DBContext;

        public PublisherController(AppDBContext context)
        {
            _DBContext = context;
        }

        //GET: api/Publisher
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PublisherWithSongsAndArtists>>> GetPublishers()
        {
            var _publishers = await _DBContext.Publishers.ToListAsync();
            var _pubWithSongsAndArtist = new List<PublisherWithSongsAndArtists>();

            foreach (var pub in _publishers)
            {
                _pubWithSongsAndArtist.Add(await _DBContext.Publishers
                    .Where(p => p.Id == pub.Id).Select(pub => new PublisherWithSongsAndArtists()
                    {
                        Id = pub.Id,
                        Name = pub.Name,
                        SongArtists = pub.Songs.Select(song => new SongArtistViewModel()
                        {
                            Id = song.Id,
                            SongName = song.Title,
                            SongArtists = song.Song_Artists.Select(sa => sa.Artist.Name).ToList()
                        }).ToList()
                    }).FirstOrDefaultAsync()
                );
            }

            return _pubWithSongsAndArtist;
        }


        // GET: api/Publisher/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PublisherWithSongsAndArtists>> GetPublisher(int id)
        {
            var _publisherData = await _DBContext.Publishers
                .Where(p => p.Id == id).Select(pub => new PublisherWithSongsAndArtists()
                {
                    Id = pub.Id,
                    Name = pub.Name,
                    SongArtists = pub.Songs.Select(song => new SongArtistViewModel()
                    {
                        Id = song.Id,
                        SongName = song.Title,
                        SongArtists = song.Song_Artists.Select(sa => sa.Artist.Name).ToList()
                    }).ToList()
                }).FirstOrDefaultAsync();

            return _publisherData;
        }

        // PUT: api/Publisher/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPublisher(int id, PublisherViewModel publisherVM)
        {
            var _publisher = new Publisher()
            {
                Id = id,
                Name = publisherVM.Name
            };


            _DBContext.Entry(_publisher).State = EntityState.Modified;

            try
            {
                await _DBContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PublisherExists(id))
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

        // POST: api/Publisher
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
        }

        private bool PublisherExists(int id)
        {
            return _DBContext.Publishers.Any(e => e.Id == id);
        }
    }
}

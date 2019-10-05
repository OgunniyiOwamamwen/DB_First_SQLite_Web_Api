using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DB_First_SQLite_Web_Api.Models;

namespace DB_First_SQLite_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistTracksController : ControllerBase
    {
        private readonly chinookContext _context;

        public PlaylistTracksController(chinookContext context)
        {
            _context = context;
        }

        // GET: api/PlaylistTracks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlaylistTrack>>> GetPlaylistTrack()
        {
            return await _context.PlaylistTrack.ToListAsync();
        }

        // GET: api/PlaylistTracks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlaylistTrack>> GetPlaylistTrack(long id)
        {
            var playlistTrack = await _context.PlaylistTrack.FindAsync(id);

            if (playlistTrack == null)
            {
                return NotFound();
            }

            return playlistTrack;
        }

        // PUT: api/PlaylistTracks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlaylistTrack(long id, PlaylistTrack playlistTrack)
        {
            if (id != playlistTrack.PlaylistId)
            {
                return BadRequest();
            }

            _context.Entry(playlistTrack).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlaylistTrackExists(id))
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

        // POST: api/PlaylistTracks
        [HttpPost]
        public async Task<ActionResult<PlaylistTrack>> PostPlaylistTrack(PlaylistTrack playlistTrack)
        {
            _context.PlaylistTrack.Add(playlistTrack);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PlaylistTrackExists(playlistTrack.PlaylistId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetPlaylistTrack", new { id = playlistTrack.PlaylistId }, playlistTrack);
        }

        // DELETE: api/PlaylistTracks/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PlaylistTrack>> DeletePlaylistTrack(long id)
        {
            var playlistTrack = await _context.PlaylistTrack.FindAsync(id);
            if (playlistTrack == null)
            {
                return NotFound();
            }

            _context.PlaylistTrack.Remove(playlistTrack);
            await _context.SaveChangesAsync();

            return playlistTrack;
        }

        private bool PlaylistTrackExists(long id)
        {
            return _context.PlaylistTrack.Any(e => e.PlaylistId == id);
        }
    }
}

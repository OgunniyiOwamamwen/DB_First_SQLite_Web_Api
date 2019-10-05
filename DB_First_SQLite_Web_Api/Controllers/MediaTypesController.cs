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
    public class MediaTypesController : ControllerBase
    {
        private readonly chinookContext _context;

        public MediaTypesController(chinookContext context)
        {
            _context = context;
        }

        // GET: api/MediaTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MediaTypes>>> GetMediaTypes()
        {
            return await _context.MediaTypes.ToListAsync();
        }

        // GET: api/MediaTypes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MediaTypes>> GetMediaTypes(long id)
        {
            var mediaTypes = await _context.MediaTypes.FindAsync(id);

            if (mediaTypes == null)
            {
                return NotFound();
            }

            return mediaTypes;
        }

        // PUT: api/MediaTypes/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMediaTypes(long id, MediaTypes mediaTypes)
        {
            if (id != mediaTypes.MediaTypeId)
            {
                return BadRequest();
            }

            _context.Entry(mediaTypes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MediaTypesExists(id))
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

        // POST: api/MediaTypes
        [HttpPost]
        public async Task<ActionResult<MediaTypes>> PostMediaTypes(MediaTypes mediaTypes)
        {
            _context.MediaTypes.Add(mediaTypes);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MediaTypesExists(mediaTypes.MediaTypeId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMediaTypes", new { id = mediaTypes.MediaTypeId }, mediaTypes);
        }

        // DELETE: api/MediaTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<MediaTypes>> DeleteMediaTypes(long id)
        {
            var mediaTypes = await _context.MediaTypes.FindAsync(id);
            if (mediaTypes == null)
            {
                return NotFound();
            }

            _context.MediaTypes.Remove(mediaTypes);
            await _context.SaveChangesAsync();

            return mediaTypes;
        }

        private bool MediaTypesExists(long id)
        {
            return _context.MediaTypes.Any(e => e.MediaTypeId == id);
        }
    }
}

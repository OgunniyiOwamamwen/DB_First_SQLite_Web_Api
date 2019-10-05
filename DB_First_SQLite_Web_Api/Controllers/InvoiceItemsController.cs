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
    public class InvoiceItemsController : ControllerBase
    {
        private readonly chinookContext _context;

        public InvoiceItemsController(chinookContext context)
        {
            _context = context;
        }

        // GET: api/InvoiceItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<InvoiceItems>>> GetInvoiceItems()
        {
            return await _context.InvoiceItems.ToListAsync();
        }

        // GET: api/InvoiceItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<InvoiceItems>> GetInvoiceItems(long id)
        {
            var invoiceItems = await _context.InvoiceItems.FindAsync(id);

            if (invoiceItems == null)
            {
                return NotFound();
            }

            return invoiceItems;
        }

        // PUT: api/InvoiceItems/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoiceItems(long id, InvoiceItems invoiceItems)
        {
            if (id != invoiceItems.InvoiceLineId)
            {
                return BadRequest();
            }

            _context.Entry(invoiceItems).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceItemsExists(id))
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

        // POST: api/InvoiceItems
        [HttpPost]
        public async Task<ActionResult<InvoiceItems>> PostInvoiceItems(InvoiceItems invoiceItems)
        {
            _context.InvoiceItems.Add(invoiceItems);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (InvoiceItemsExists(invoiceItems.InvoiceLineId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetInvoiceItems", new { id = invoiceItems.InvoiceLineId }, invoiceItems);
        }

        // DELETE: api/InvoiceItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<InvoiceItems>> DeleteInvoiceItems(long id)
        {
            var invoiceItems = await _context.InvoiceItems.FindAsync(id);
            if (invoiceItems == null)
            {
                return NotFound();
            }

            _context.InvoiceItems.Remove(invoiceItems);
            await _context.SaveChangesAsync();

            return invoiceItems;
        }

        private bool InvoiceItemsExists(long id)
        {
            return _context.InvoiceItems.Any(e => e.InvoiceLineId == id);
        }
    }
}

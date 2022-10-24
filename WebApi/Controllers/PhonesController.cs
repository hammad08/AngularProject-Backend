using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhonesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PhonesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Phones
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Phone>>> Getphones()
        {
            return await _context.phones.ToListAsync();
        }

        // GET: api/Phones/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Phone>> GetPhone(int id)
        {
            var phone = await _context.phones.FindAsync(id);

            if (phone == null)
            {
                return NotFound();
            }

            return phone;
        }

        // PUT: api/Phones/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPhone(int id, Phone phone)
        {
            if (id != phone.phoneId)
            {
                return BadRequest();
            }

            _context.Entry(phone).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PhoneExists(id))
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

        // POST: api/Phones
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Phone>> PostPhone(Phone phone)
        {
            _context.phones.Add(phone);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPhone", new { id = phone.phoneId }, phone);
        }

        // DELETE: api/Phones/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhone(int id)
        {
            var phone = await _context.phones.FindAsync(id);
            if (phone == null)
            {
                return NotFound();
            }

            _context.phones.Remove(phone);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PhoneExists(int id)
        {
            return _context.phones.Any(e => e.phoneId == id);
        }
    }
}

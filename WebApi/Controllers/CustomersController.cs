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
    public class CustomersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CustomersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Customers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> Getcustomers()
        {
            return await _context.customers
                .Include(b => b.addresses)
                .ThenInclude( p => p.phones)
                .ToListAsync();
        }

        // GET: api/Customers/firstName
        [HttpGet("byFullName/{firstName}/{lastName}")]
        public async Task<ActionResult<Customer>> GetCustomerByFirstName(string firstName, string lastName)
        {
            var customer = await _context.customers.Where(d => d.firstName==firstName && d.lastName==lastName)
                .Include(b => b.addresses)
                .ThenInclude(p => p.phones)
                .FirstOrDefaultAsync();

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        // GET: api/Customers/lastName
        [HttpGet("byLastName/{lastName}")]
        public async Task<ActionResult<Customer>> GetCustomerByLastName(string lastName)
        {
            var customer = await _context.customers.FirstOrDefaultAsync(h => h.lastName == lastName);

            if (customer == null)
            {
                return NotFound();
            }

            return customer;
        }

        private EntityState ToEntityState(state state)
        {
            switch(state)
            {
                case state.Unchanged:
                    return EntityState.Unchanged;
                case state.Added:
                    return EntityState.Added;
                case state.Modified:
                    return EntityState.Modified;
                case state.Deleted:
                    return EntityState.Deleted;
                default:
                    return EntityState.Unchanged;

            }

        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutCustomer(int id, Customer customer)
        //{

            

        //    ////logic#01
        //    //_context.Entry(customer).State = ToEntityState(customer.StateEnum);

        //    //customer.addresses.ForEach(a =>
        //    //{
        //    //    if (a.addressId == 0) a.StateEnum = state.Added;
        //    //    _context.Entry(a).State = ToEntityState(a.StateEnum);
        //    //    a.phones.ForEach(p =>
        //    //    {
        //    //        if (p.phoneId == 0) p.StateEnum = state.Added;
        //    //        _context.Entry(p).State = ToEntityState(p.StateEnum);
        //    //    });
        //    //});
            
        //    //if (id != customer.customerId)
        //    //{
        //    //    return BadRequest();
        //    //}


        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //        return Ok(customer);
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!CustomerExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Customer>> PostCustomer(Customer customer)
        {
            if (customer.customerId == 0)
            {
                await _context.customers.AddAsync(customer);
            }
            else
            {
                _context.ChangeTracker.TrackGraph(customer, p =>
                {
                    var entity = (BaseModel)p.Entry.Entity;
                    p.Entry.State = entity.StateEnum == state.Added ? EntityState.Added :
                    entity.StateEnum == state.Modified ? EntityState.Modified :
                    entity.StateEnum == state.Deleted ? EntityState.Deleted : EntityState.Unchanged;
                });
            }

            await _context.SaveChangesAsync();
            return Ok(customer);
        }

        // DELETE: api/Customers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customer = await _context.customers.FindAsync(id);
            if (customer == null)
            {
                return NotFound();
            }

            _context.customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CustomerExists(int id)
        {
            return _context.customers.Any(e => e.customerId == id);
        }
    }


}

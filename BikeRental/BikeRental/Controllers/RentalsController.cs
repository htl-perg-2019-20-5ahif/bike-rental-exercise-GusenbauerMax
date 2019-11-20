using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BikeRental;
using BikeRental.Models;
using BikeRental.Services;

namespace BikeRental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly BikeRentalContext _context;
        private ICostCalculator costCalculator;

        public RentalsController(BikeRentalContext context, ICostCalculator costCalculator)
        {
            _context = context;
            this.costCalculator = costCalculator;
        }

        // GET: api/Rentals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rental>>> GetRentals()
        {
            return await _context.Rentals.ToListAsync();
        }

        [HttpGet]
        [Route("Unpaid")]
        public async Task<ActionResult<IEnumerable<Rental>>> GetUnpaidRentals()
        {
            return await _context.Rentals
                .Include(r => r.Customer)
                .Where(r => r.Paid == false && r.TotalCost > 0)
                .ToArrayAsync();
        }

        // GET: api/Rentals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Rental>> GetRental(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);

            if (rental == null)
            {
                return NotFound();
            }

            return rental;
        }

        // PUT: api/Rentals/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> EndRental(int id)
        {
            var rental = await _context.Rentals.Include(r => r.Bike).FirstOrDefaultAsync(r => r.RentalID == id);

            if (rental == null || rental.End != DateTime.MinValue || rental.TotalCost != -1)
            {
                return BadRequest();
            }

            rental.End = DateTime.Now;
            rental.TotalCost = costCalculator.CalculateCosts(rental.Begin, rental.End, rental.Bike.RentalBasic, rental.Bike.RentalExcessive);

            _context.Entry(rental).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(id))
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

        // POST: api/Rentals
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Rental>> StartRental(Rental rental)
        {
            rental.Begin = DateTime.Now;
            rental.TotalCost = -1;
            rental.End = DateTime.MinValue;
            rental.Paid = false;
            _context.Rentals.Add(rental);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RentalExists(rental.BikeID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRental", new { id = rental.BikeID }, rental);
        }

        [HttpPut("Pay/{id}")]
        public async Task<IActionResult> MarkRentalAsPaid(int id)
        {
            var rental = await _context.Rentals.FirstOrDefaultAsync(r => r.RentalID == id);

            if (rental == null || rental.End == DateTime.MinValue || rental.TotalCost == -1)
            {
                return BadRequest();
            }

            rental.Paid = true;

            _context.Entry(rental).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(id))
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

        // DELETE: api/Rentals/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Rental>> DeleteRental(int id)
        {
            Rental rental = await _context.Rentals.FirstOrDefaultAsync(r => r.RentalID == id);
            if (rental == null)
            {
                return NotFound();
            }

            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();

            return rental;
        }

        private bool RentalExists(int id)
        {
            return _context.Rentals.Any(e => e.BikeID == id);
        }
    }
}

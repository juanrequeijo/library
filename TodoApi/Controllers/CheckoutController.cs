using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private readonly CheckoutContext _context;

        public CheckoutController(CheckoutContext context)
        {
            _context = context;

            if (_context.CheckoutItems.Count() == 0)
            {
                // Create a new CheckoutItem if collection is empty,
                // which means you can't delete all CheckoutItems.
                _context.CheckoutItems.Add(new CheckoutItem { Name = "Juan Requeijo" });
                _context.SaveChanges();
            }
        }
        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CheckoutItem>>> GetCheckoutItems()
        {
            return await _context.CheckoutItems.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CheckoutItem>> GetCheckoutItem(long id)
        {
            var CheckoutItem = await _context.CheckoutItems.FindAsync(id);

            if (CheckoutItem == null)
            {
                return NotFound();
            }

            return CheckoutItem;
        }

        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<CheckoutItem>> PostCheckoutItem(CheckoutItem item)
        {
            _context.CheckoutItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCheckoutItem), new { id = item.Id }, item);
        }

        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCheckoutItem(long id, CheckoutItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }


        /// <summary>
        /// Deletes a specific CheckoutItem.
        /// </summary>
        /// <param name="id"></param>
        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCheckoutItem(long id)
        {
            var CheckoutItem = await _context.CheckoutItems.FindAsync(id);

            if (CheckoutItem == null)
            {
                return NotFound();
            }

            _context.CheckoutItems.Remove(CheckoutItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
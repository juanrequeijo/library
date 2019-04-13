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
    public class StatusController : ControllerBase
    {
        private readonly StatusContext _context;

        public StatusController(StatusContext context)
        {
            _context = context;

            if (_context.StatusItems.Count() == 0)
            {
                // Create a new StatusItem if collection is empty,
                // which means you can't delete all StatusItems.
                _context.StatusItems.Add(new StatusItem { Name = "Juan Requeijo" });
                _context.SaveChanges();
            }
        }
        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusItem>>> GetStatusItems()
        {
            return await _context.StatusItems.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StatusItem>> GetStatusItem(long id)
        {
            var StatusItem = await _context.StatusItems.FindAsync(id);

            if (StatusItem == null)
            {
                return NotFound();
            }

            return StatusItem;
        }

        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<StatusItem>> PostStatusItem(StatusItem item)
        {
            _context.StatusItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetStatusItem), new { id = item.Id }, item);
        }

        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatusItem(long id, StatusItem item)
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
        /// Deletes a specific StatusItem.
        /// </summary>
        /// <param name="id"></param>
        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatusItem(long id)
        {
            var StatusItem = await _context.StatusItems.FindAsync(id);

            if (StatusItem == null)
            {
                return NotFound();
            }

            _context.StatusItems.Remove(StatusItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
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
    public class DemandsController : ControllerBase
    {
        private readonly DemandsContext _context;

        public DemandsController(DemandsContext context)
        {
            _context = context;

            if (_context.DemandsItems.Count() == 0)
            {
                // Create a new DemandsItem if collection is empty,
                // which means you can't delete all DemandsItems.
                _context.DemandsItems.Add(new DemandsItem { Name = "Juan Requeijo" });
                _context.SaveChanges();
            }
        }
        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DemandsItem>>> GetDemandsItems()
        {
            return await _context.DemandsItems.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DemandsItem>> GetDemandsItem(long id)
        {
            var DemandsItem = await _context.DemandsItems.FindAsync(id);

            if (DemandsItem == null)
            {
                return NotFound();
            }

            return DemandsItem;
        }

        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<DemandsItem>> PostDemandsItem(DemandsItem item)
        {
            _context.DemandsItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDemandsItem), new { id = item.Id }, item);
        }

        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDemandsItem(long id, DemandsItem item)
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
        /// Deletes a specific DemandsItem.
        /// </summary>
        /// <param name="id"></param>
        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDemandsItem(long id)
        {
            var DemandsItem = await _context.DemandsItems.FindAsync(id);

            if (DemandsItem == null)
            {
                return NotFound();
            }

            _context.DemandsItems.Remove(DemandsItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
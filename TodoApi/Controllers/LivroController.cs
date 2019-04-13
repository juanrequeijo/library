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
    public class LivroController : ControllerBase
    {
        private readonly LivroContext _context;

        public LivroController(LivroContext context)
        {
            _context = context;

            if (_context.LivroItems.Count() == 0)
            {
                // Create a new LivroItem if collection is empty,
                // which means you can't delete all LivroItems.
                _context.LivroItems.Add(new LivroItem { Name = "Juan Requeijo" });
                _context.SaveChanges();
            }
        }
        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LivroItem>>> GetLivroItems()
        {
            return await _context.LivroItems.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LivroItem>> GetLivroItem(long id)
        {
            var LivroItem = await _context.LivroItems.FindAsync(id);

            if (LivroItem == null)
            {
                return NotFound();
            }

            return LivroItem;
        }

        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<LivroItem>> PostLivroItem(LivroItem item)
        {
            _context.LivroItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetLivroItem), new { id = item.Id }, item);
        }

        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLivroItem(long id, LivroItem item)
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
        /// Deletes a specific LivroItem.
        /// </summary>
        /// <param name="id"></param>
        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLivroItem(long id)
        {
            var LivroItem = await _context.LivroItems.FindAsync(id);

            if (LivroItem == null)
            {
                return NotFound();
            }

            _context.LivroItems.Remove(LivroItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
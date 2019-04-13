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
    public class SearchController : ControllerBase
    {
        private readonly SearchContext _context;

        public SearchController(SearchContext context)
        {
            _context = context;

            if (_context.SearchItems.Count() == 0)
            {
                // Create a new SearchItem if collection is empty,
                // which means you can't delete all SearchItems.
                _context.SearchItems.Add(new SearchItem { Name = "Juan Requeijo" });
                _context.SaveChanges();
            }
        }
        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SearchItem>>> GetSearchItems()
        {
            return await _context.SearchItems.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SearchItem>> GetSearchItem(long id)
        {
            var SearchItem = await _context.SearchItems.FindAsync(id);

            if (SearchItem == null)
            {
                return NotFound();
            }

            return SearchItem;
        }

        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<SearchItem>> PostSearchItem(SearchItem item)
        {
            _context.SearchItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSearchItem), new { id = item.Id }, item);
        }

        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSearchItem(long id, SearchItem item)
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
        /// Deletes a specific SearchItem.
        /// </summary>
        /// <param name="id"></param>
        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSearchItem(long id)
        {
            var SearchItem = await _context.SearchItems.FindAsync(id);

            if (SearchItem == null)
            {
                return NotFound();
            }

            _context.SearchItems.Remove(SearchItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
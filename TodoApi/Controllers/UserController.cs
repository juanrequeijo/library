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
    public class UserController : ControllerBase
    {
        private readonly UserContext _context;

        public UserController(UserContext context)
        {
            _context = context;

            if (_context.UserItems.Count() == 0)
            {
                // Create a new UserItem if collection is empty,
                // which means you can't delete all UserItems.
                _context.UserItems.Add(new UserItem { Name = "Juan Requeijo" });
                _context.SaveChanges();
            }
        }
        // GET: api/Todo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserItem>>> GetUserItems()
        {
            return await _context.UserItems.ToListAsync();
        }

        // GET: api/Todo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserItem>> GetUserItem(long id)
        {
            var UserItem = await _context.UserItems.FindAsync(id);

            if (UserItem == null)
            {
                return NotFound();
            }

            return UserItem;
        }

        // POST: api/Todo
        [HttpPost]
        public async Task<ActionResult<UserItem>> PostUserItem(UserItem item)
        {
            _context.UserItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUserItem), new { id = item.Id }, item);
        }

        // PUT: api/Todo/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserItem(long id, UserItem item)
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
        /// Deletes a specific UserItem.
        /// </summary>
        /// <param name="id"></param>   
        // DELETE: api/Todo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserItem(long id)
        {
            var UserItem = await _context.UserItems.FindAsync(id);

            if (UserItem == null)
            {
                return NotFound();
            }

            _context.UserItems.Remove(UserItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }


    }
}
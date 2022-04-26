using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIKurs.Data;
using WebAPIKurs.Models;

namespace WebAPIKurs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiConventionType(typeof(DefaultApiConventions))] //Default-Convention für eine komplette Controller - Klasse 
    public class ConventionsController : ControllerBase
    {
        private readonly ToDoDbContext _context;

        public ConventionsController(ToDoDbContext context)
        {
            _context = context;
        }

        // GET: api/ToDoItems
        [HttpGet]

        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))] // Zusammenfassung von ProducesResponseType
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItem()
        {
            return await _context.ToDoItem.ToListAsync();
        }

        /// <summary>
        /// Erhalte eine Liste von TodoItems
        /// </summary>
        /// <returns>ToDoItem - Liste</returns>
        [HttpGet("GetToDoItemList")]
        public async IAsyncEnumerable<ToDoItem> GetToDoItemList()
        {
            IAsyncEnumerable<ToDoItem> todoList = _context.ToDoItem.AsAsyncEnumerable();


            await foreach (ToDoItem currentToDo in todoList)
                yield return currentToDo;
        }

        // GET: api/ToDoItems/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ToDoItem), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ToDoItem), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ToDoItem), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ToDoItem), StatusCodes.Status429TooManyRequests)]
        public async Task<ActionResult<ToDoItem>> GetToDoItem(int id)
        {
            var toDoItem = await _context.ToDoItem.FindAsync(id);

            if (toDoItem == null)
            {
                return NotFound();
            }

            return toDoItem;
        }

        // PUT: api/ToDoItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754

        /// <summary>
        /// Aktualisiere eine Aufgabe
        /// </summary>
        /// <param name="id">id der Aufgabe</param>
        /// <param name="toDoItem">Aufgaben JsonObject</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutToDoItem(int id, ToDoItem toDoItem)
        {
            if (id != toDoItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(toDoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToDoItemExists(id))
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

        // POST: api/ToDoItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        /// <summary>
        /// Hinzufügen einer Aufgabe
        /// </summary>
        /// <param name="toDoItem"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<ToDoItem>> PostToDoItem(ToDoItem toDoItem)
        {
            _context.ToDoItem.Add(toDoItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetToDoItem", new { id = toDoItem.Id }, toDoItem);
        }

        // DELETE: api/ToDoItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteToDoItem(int id)
        {
            var toDoItem = await _context.ToDoItem.FindAsync(id);
            if (toDoItem == null)
            {
                return NotFound();
            }

            _context.ToDoItem.Remove(toDoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ToDoItemExists(int id)
        {
            return _context.ToDoItem.Any(e => e.Id == id);
        }
    }
}

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIKurs.Data;
using WebAPIKurs.Models;

namespace WebAPIKurs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PagingAndSortingController : ControllerBase
    {
        private readonly ToDoDbContext _context;

        public PagingAndSortingController(ToDoDbContext context)
        {
            _context = context;
        }

        
        // GET: api/ToDoItems
        [HttpGet("ToDoListWithPaging")]
        public async Task<ActionResult<IEnumerable<ToDoItem>>> GetToDoItems(int pageNumber = 1, int pageSize = 3)
        {
            return await _context.ToDoItem.OrderBy(e=>e.Name)
                                          .Skip((pageNumber - 1) * pageSize)
                                          .Take(pageSize).ToListAsync();
        }
    }
}

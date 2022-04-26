using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIKurs.Models;

namespace WebAPIKurs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        [HttpPost("InsertToDoItem")]
        public IActionResult InsertToDoItem(ToDoItem toDoItem)
        {
            return Ok();
        }

        [HttpPost("InserToDoItemAsQuery")]
        public IActionResult InsertToDoItemAsQuery([FromQuery] ToDoItem toDoItem)
        {
            return Ok();
        }



        [HttpPost("InsertString")]
        public IActionResult InsertString1(string name) //ist automatisch ein Query-String mit ?name=otto
        {
            return Ok();
        }

        [HttpPost("InsertString2/{name}")] // Erweiterung des Seqmentes
        public IActionResult InsertString2(string name) //ist automatisch ein Query-String mit ?name=otto
        {
            return Ok();
        }

        [HttpPost("InsertString3")]
        public IActionResult InsertString3([FromBody] string name)
        {
            return Ok();
        }


        





    }
}

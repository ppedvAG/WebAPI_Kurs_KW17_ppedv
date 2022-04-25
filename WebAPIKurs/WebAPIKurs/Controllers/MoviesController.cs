#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPIKurs.Data;
using WebAPIKurs.Models;
using WebAPIKurs.Services;

namespace WebAPIKurs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly MovieDbContext _context;

        private readonly IMultipleDatabaseService _service;
       
        
        public MoviesController(IConfiguration configuration) //Pro Request wird Controller neu instanziiert -> Scoped
        {
            _service = new MultipleDatabaseService(configuration);

        }

        // GET: api/Movies/GetMovies
        [HttpGet("GetMovies")]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovie()
        {
            return await _context.Movie.ToListAsync();
        }

        // GET: api/Movies/GetMovie

        //[HttpGet("{id}")]
        [HttpGet("GetMovie")]
        public async Task<ActionResult<Movie>> GetMovie(int id, int standort) //QueryString
        {
            //string conStr = string.Empty;

            //if (standort == 1)
            //{
            //    conStr = _configuration["ConnectionStrings:MovieDbContext"];
            //}
            //else
            //    conStr = _configuration["ConnectionStrings:MovieDbContext2"];

            //var options = new DbContextOptionsBuilder<MovieDbContext>().UseSqlServer(conStr).Options;
            //Movie movie = null;

            //using (MovieDbContext context = new MovieDbContext(options))
            //{
            //    movie = context.Movie.FirstOrDefault(e => e.Id == id);
            //}




            Movie movie; 

            movie = _service.GetMovie(id, standort);

            if (movie == null)
                return BadRequest();

            return Ok(movie);
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
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

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}")]
        public async Task<ActionResult<Movie>> PostMovie(int id, Movie movie)
        {
            //string conStr = string.Empty;
            //if (id == 1)
            //{
            //    conStr = _configuration["ConnectionStrings:MovieDbContext"];
            //}
            //else
            //    conStr = _configuration["ConnectionStrings:MovieDbContext2"];

            //var options = new DbContextOptionsBuilder<MovieDbContext>().UseSqlServer(conStr).Options;

            //using (MovieDbContext context = new MovieDbContext(options))
            //{
            //    context.Movie.Add(movie);
            //    await context.SaveChangesAsync();
            //}

            movie = _service.InsertMovie(id, movie);




            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movie.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movie.Any(e => e.Id == id);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using WebAPIKurs.Data;
using WebAPIKurs.Models;

namespace WebAPIKurs.Services
{
    public class MultipleDatabaseService : IMultipleDatabaseService
    {
        private readonly IConfiguration _configuration;
        //private readonly int _standort;
        public MultipleDatabaseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private string GetCurrentConnectionString(int standort)
        {
            string conStr = string.Empty;

            if (standort == 1)
            {
                conStr = _configuration["ConnectionStrings:MovieDbContext"];
            }
            else
                conStr = _configuration["ConnectionStrings:MovieDbContext2"];

            return conStr;
        }

        private DbContextOptions<MovieDbContext> GetMovieDbContext(int standort)
        {
            var options = new DbContextOptionsBuilder<MovieDbContext>().UseSqlServer(GetCurrentConnectionString(standort)).Options;

            return options;
        }
        
        public Movie GetMovie(int id, int standort)
        {

            Movie movie = null;

            using (MovieDbContext context = new MovieDbContext(GetMovieDbContext(standort)))
            {
                movie = context.Movie.FirstOrDefault(e => e.Id == id);
            }


            return movie;
        }

        
        public Movie InsertMovie(int standort, Movie movie)
        {
            using (MovieDbContext context = new MovieDbContext(GetMovieDbContext(standort)))
            {
                context.Movie.Add(movie);
                context.SaveChanges();
            }
            return movie;
        }

        
    }
}

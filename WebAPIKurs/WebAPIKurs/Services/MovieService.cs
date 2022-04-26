using Microsoft.EntityFrameworkCore;
using WebAPIKurs.Data;
using WebAPIKurs.Models;

namespace WebAPIKurs.Services
{
    public class MovieService : GenericServicey<Movie>, IMovieService
    {
        public MovieService(IConfiguration configuration)
            :base(configuration)
        {
            
        }

        public override Movie Get(int id, int standort)
        {
            Movie movie = null;


            //IMovieRepository repository = new MovieRepository
            //Movie = repository.GetById(id, standort) 


            // Logik 

            using (MovieDbContext context = new MovieDbContext(GetMovieDbContext(standort)))
            {
                movie = context.Movie.FirstOrDefault(e => e.Id == id);
            }


            return movie;
        }

        public override List<Movie> GetAll(int standort)
        {
            using MovieDbContext context = new MovieDbContext(GetMovieDbContext(standort));


            return context.Movie.ToList();

        }

        public override Movie Insert(int id, Movie entity)
        {
            using (MovieDbContext context = new MovieDbContext(GetMovieDbContext(id)))
            {
                context.Movie.Add(entity);
                context.SaveChanges();
            }
            return entity;
        }
    }
}

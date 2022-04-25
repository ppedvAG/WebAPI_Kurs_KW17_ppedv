using WebAPIKurs.Models;

namespace WebAPIKurs.Services
{
    public interface IMultipleDatabaseService
    {

         Movie GetMovie(int id, int standort);

        Movie InsertMovie(int standort, Movie movie);
    }
}

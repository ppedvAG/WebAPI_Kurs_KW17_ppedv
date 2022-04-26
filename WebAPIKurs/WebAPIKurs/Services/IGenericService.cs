using Microsoft.EntityFrameworkCore;
using WebAPIKurs.Data;

namespace WebAPIKurs.Services
{
    public interface IGenericServicey<T> where T : class
    {
        List<T> GetAll(int standort);
        T Get(int id, int Standort);

        T Insert(int id, T entity);
    }

    public abstract class GenericServicey<T> : IGenericServicey<T> where T : class
    {
        IConfiguration _configuration;
        public GenericServicey(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public abstract List<T> GetAll(int standort);
        public abstract T Get(int id, int Standort);

        public abstract T Insert(int id, T entity);

        protected string GetCurrentConnectionString(int standort)
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

        protected DbContextOptions<MovieDbContext> GetMovieDbContext(int standort)
        {
            var options = new DbContextOptionsBuilder<MovieDbContext>().UseSqlServer(GetCurrentConnectionString(standort)).Options;

            return options;
        }

    }


    
}

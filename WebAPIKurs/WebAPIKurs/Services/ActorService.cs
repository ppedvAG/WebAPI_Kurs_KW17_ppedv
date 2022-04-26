using WebAPIKurs.Models;

namespace WebAPIKurs.Services
{
    public class ActorService : GenericServicey<Actor>, IActorService
    {
        public ActorService(IConfiguration configuration)
            : base(configuration)
        {
        }
        public override Actor Get(int id, int Standort)
        {
            throw new NotImplementedException();
        }

        public override List<Actor> GetAll(int standort)
        {
            throw new NotImplementedException();
        }

        public override Actor Insert(int id, Actor entity)
        {
            throw new NotImplementedException();
        }

        
    }
}

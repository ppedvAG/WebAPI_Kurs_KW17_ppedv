using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIKurs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DIController : ControllerBase
    {
        private readonly ICar mockCar;


        //ctor + tab + tab

        //Konstruktor Injection
        public DIController(ICar mockCar)
        {
            //mockCar -> VW Polo
            this.mockCar = mockCar;
        }

        [HttpGet("GetMockCar")]
        public ICar GetMockCar([FromServices] ICar car)
        {
            return car;
        }

        [HttpGet("GetMockCarOverHttpContet")]
        public ICar GetMockCarOverHttpContet()
        {
            //3.1 oder höher 
            ICar mockCar = HttpContext.RequestServices.GetService<ICar>();


            //.NET 2.1 
            IServiceScope scope = HttpContext.RequestServices.CreateScope();
            ICar mockCar2 = scope.ServiceProvider.GetService<ICar>();

            return mockCar;
        }

        [HttpGet("HelloWorld1")]
        public string HelloWorld1()
        {
            return "Hello World";
        }


        [HttpGet("HelloWorld2")]
        public ContentResult HelloWorld2()
        {
            return Content("Hallo Welt2"); //gibt auch einen string zurück 
        }
    }
}

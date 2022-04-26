using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIKurs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReturnTypesController : ControllerBase
    {
        #region Native Datentypen
        [HttpGet]
        public string HelloWorld()
        {
            return "Hello World";
        }


        //Achtung Routing
        [HttpGet("/GetCurrentTime")]
        public ContentResult GetCurrenTime()
        {
            return Content(DateTime.Now.ToString());
        }

        [HttpGet("GetCurrentTime2")] 
        public ContentResult GetCurrenTime2()
        {
            return Content(DateTime.Now.ToString());
        }
        #endregion

        #region KomplexeObjekte
        [HttpGet("GetComplexObject")]
        //Komplexe Objekte werden als JSON Serialisiert und zurück gegeben
        public Car GetComplexObject()
        {
            Car car = new();
            car.Id = 1;
            car.Marke = "Porsche";
            car.Modell = "911er";

            return car; //Das ist die einziste Möglichkeit etwas zurück zu geben. Und nur vom Typ 'Car'
        }
        #endregion


        #region IActionResult + ActionResult

        //Synchron
        [HttpGet("GetCarWith_IActionResult")]
        public IActionResult GetCarWith_IActionResult()
        {
            Car car = new();
            car.Id = 1;
            car.Marke = "Porsche";
            car.Modell = "911er";

            if (car.Marke != "Porsche")
                return BadRequest(); //400

            if (car.Marke == String.Empty)
                return NotFound(); //404

            return Ok(car); //200 bei Get
        }


        [HttpGet("GetCarWith_ActionResult")]
        public ActionResult GetCarWith_ActionResult()
        {
            Car car = new();
            car.Id = 1;
            car.Marke = "Porsche";
            car.Modell = "911er";

            if (car.Marke != "Porsche")
                return BadRequest(); //400

            if (car.Modell == String.Empty)
                return NotFound(); //404

            return Ok(car); //200 bei Get
        }

        //Asynchron 

        // Asynchrone Methoden mit async / await
        [HttpGet("GetCarWith_IActionResultAsync")]
        public async Task<IActionResult> GetCarWith_IActionResultAsync()
        {
            await Task.Delay(1000);
            Car car = new();
            car.Id = 1;
            car.Marke = "Porsche";
            car.Modell = "911er";

            if (car.Marke != "Prosche")
                return BadRequest(); //400

            if (car.Marke == String.Empty)
                return NotFound(); //404

            return Ok(car); //200 bei Get
        }

        [HttpGet("GetCarWith_ActionResultAsync/{id}")]
        public async Task<ActionResult> GetCarWith_ActionResultAsync(int id)
        {
            await Task.Delay(1000);
            Car car = new();
            car.Id = 1;
            car.Marke = "Porsche";
            car.Modell = "911er";

            if (car.Marke != "Prosche")
                return BadRequest(); //400

            if (car.Marke == String.Empty)
                return NotFound(); //404

            return Ok(car); //200 bei Get
        }

        #endregion

        #region Serialisierung
        [HttpGet("GetCarIEnumerable")]
        public IEnumerable<Car> GetCarIEnumerable()
        {
            Car car = new();
            car.Id = 1;
            car.Marke = "Porsche";
            car.Modell = "911er";


            Car car1 = new();
            car1.Id = 2;
            car1.Marke = "Audi";
            car1.Modell = "Quatro";

            Car car2 = new();
            car2.Id = 3;
            car2.Marke = "VW";
            car2.Modell = "Polo";

            List<Car> carList = new List<Car>();
            carList.Add(car);
            carList.Add(car1);
            carList.Add(car2);


            foreach (Car currentCar in carList)
                yield return currentCar; //Wir bleiben in der Schleife und geben jeden einzelenen Datensatz raus. 


        } //Hier verlassen wir die Methode


        //Erst Ab ASP.NET Core 3.1 -> Fehlerfrei 
        [HttpGet("GetCarIAsyncEnumerable")]
        public async IAsyncEnumerable<Car> GetCarIEnumerableAsync()
        {

            Car car = new();
            car.Id = 1;
            car.Marke = "Porsche";
            car.Modell = "911er";


            Car car1 = new();
            car1.Id = 2;
            car1.Marke = "Audi";
            car1.Modell = "Quatro";

            Car car2 = new();
            car2.Id = 3;
            car2.Marke = "VW";
            car2.Modell = "Polo";

            List<Car> carList = new List<Car>();
            carList.Add(car);
            carList.Add(car1);
            carList.Add(car2);


            foreach (Car currentCar in carList)
            {
                await Task.Delay(1000);
                yield return currentCar; //Wir bleiben in der Schleife und geben jeden einzelenen Datensatz raus. 
            }



        } //Wir verlassen hier die Methode

        #endregion
    }
}

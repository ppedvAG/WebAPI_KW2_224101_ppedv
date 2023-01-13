using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Grundlagen.Models;
namespace WebAPI_Grundlagen.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReturnTypesController : ControllerBase
{

    #region Native Datentypen


    //Ausgabe eines Strings
    [HttpGet("StringSample")]
    public string HelloWorld() => "Hello World";



    [HttpGet("ConentResultSample")]
    public ContentResult HelloWorld1()
    {
        return Content("Hello World");  //Hier wird auch ein string zurück gegeben. 
    }
    #endregion


    #region Komplexe Objekte
    //Komplexe Objecte werden als JSON umgewandelt.
    //Nachteil -> Bei Fehler können keine Fehlercode zurück gegeben werden. 

    [HttpGet("GetComplexObject")]
    public Car GetComplexObject()
    {
        Car car = new();
        car.Id = 1;
        car.Brand = "VW";
        car.Model = "Polo";

        return car;
    }
    #endregion

    #region IActionResult / ActionResult ´mit Complexen Objecten 

    //IActionResult verwendet man bei: Post / Put / Delete / kann aber auch GET und bietet keinen Nachteil dabei 
    //ActionResult verwendet man bei: GET / aber kann auch POST / Put / Delete ohne Nachteil 



    [HttpGet("GetCarWith_IActionResult")]
    public IActionResult GetCarWith_IActionResult()
    {
        Car car = new Car();
        car.Id = 1;
        car.Brand = "VW";
        car.Model = "Polo";

        if (car.Id == 0)
        {
            return NotFound(); //404 Fehler
        }


        if (car.Brand == "Ford")
            return BadRequest("Ist kein Auto"); //400 


        //Geben Expizit einen 200er Code zurück und geben Car als JSON zurück
        return Ok(car);
    }

    [HttpGet("GetCarWith_ActionResult")]
    public ActionResult GetCarWith_ActionResult()
    {
        Car car = new Car();
        car.Id = 1;
        car.Brand = "VW";
        car.Model = "Ford";

        if (car.Id == 0)
        {
            return NotFound(); //404 Fehler
        }

        if (car.Model == "Ford")
            return BadRequest("Ist kein Auto"); //400 

        //Geben Expizit einen 200er Code zurück und geben Car als JSON zurück
        return Ok(car);
    }


    //Asynchrone Schreibweise

    [HttpGet("GetCarAsync_With_IActionResult")]
    public async Task<IActionResult> GetCarAsync_With_IActionResult()
    {
        Car car = new Car();
        car.Id = 1;
        car.Brand = "VW";
        car.Model = "Polo";

        if (car.Id == 0)
        {
            return NotFound(); //404 Fehler
        }


        if (car.Brand == "Ford")
            return BadRequest("Ist kein Auto"); //400 


        //Geben Expizit einen 200er Code zurück und geben Car als JSON zurück
        return Ok(car);
    }


    [HttpGet("GetCarAsync_With_ActionResult")]
    public async Task<ActionResult> GetCarAsync_With_ActionResult()
    {
        Car car = new Car();
        car.Id = 1;
        car.Brand = "VW";
        car.Model = "Polo";

        if (car.Id == 0)
        {
            return NotFound(); //404 Fehler
        }

        if (car.Brand == "Ford")
            return BadRequest("Ist kein Auto"); //400


        //Geben Expizit einen 200er Code zurück und geben Car als JSON zurück
        return Ok(car);
    }
    #endregion

    #region IEnumerable<Car> und IAsyncEnumberable 

    //Sequentielle Rückgabe einer Liste
    [HttpGet("GetCarIEnumerable")]
    public IEnumerable<Car> GetCarIEnumerable()
    {
        Car car = new();
        car.Id = 1;
        car.Brand = "Porsche";
        car.Model = "911er";


        Car car1 = new();
        car1.Id = 2;
        car1.Brand = "Audi";
        car1.Model = "Quatro";

        Car car2 = new();
        car2.Id = 3;
        car2.Brand = "VW";
        car2.Model = "Polo";

        List<Car> carList = new List<Car>();
        carList.Add(car);
        carList.Add(car1);
        carList.Add(car2);

        foreach (Car currentCar in carList)
            yield return currentCar; 

    } //Methoden läuft bis zum Ende durch -> yield return liefert im jeden Schleifen-Intervall den Wert von currentCar an den Client zurück 


    [HttpGet("GetCarIAsyncEnumerable")]
    public async IAsyncEnumerable<Car> GetCarAsyncEnumerable()
    {
        Car car = new();
        car.Id = 1;
        car.Brand = "Porsche";
        car.Model = "911er";


        Car car1 = new();
        car1.Id = 2;
        car1.Brand = "Audi";
        car1.Model = "Quatro";

        Car car2 = new();
        car2.Id = 3;
        car2.Brand = "VW";
        car2.Model = "Polo";

        List<Car> carList = new List<Car>();
        carList.Add(car);
        carList.Add(car1);
        carList.Add(car2);


        foreach (Car currentCar in carList)
        {
            await Task.Delay(1000);
            yield return currentCar; //Wir bleiben in der Schleife und geben jeden einzelenen Datensatz raus. 
        }
    }

    /*
     * 
     * Der Microsoft.AspNetCore.Http.HttpResults Namespace enthält Klassen, die die IResult Schnittstelle implementieren. 
     * Die IResult Schnittstelle definiert einen Vertrag, der das Ergebnis eines HTTP-Endpunkts darstellt. 
     * Die statische Results-Klasse wird verwendet, um unterschiedliche IResult-Objekte zu erstellen, die verschiedene Arten von Antworten darstellen.
     * 
     */



    //ab .NET 7.0 (NEUE RÜCKGABETYP) 

    [HttpGet("IResult_Sample")]
    public IResult GetById(int id)
    {
        Car car = new();
        car.Id = 1;
        car.Brand = "Porsche";
        car.Model = "911er";


        return car == null ? Results.NotFound() : Results.Ok(car);
    }

    [HttpGet("IResult_Tuple_ReturnType")]
    public Results<NotFound, Ok<Car>> GetByIdB(int id)
    {
        Car car = new();
        car.Id = 1;
        car.Brand = "Porsche";
        car.Model = "911er";

        return car == null ? TypedResults.NotFound() : TypedResults.Ok(car);
    }

    #endregion

}


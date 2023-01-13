using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI_IOC_Samples.Services;

namespace WebAPI_IOC_Samples.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeServiceController : ControllerBase
    {
        private readonly ITimeService _timeService;

        //Beispiel 1: (Dienst für ALLE)
        //Konstruktor-Injection wird verwendet um einen Dienst Klassenweit zu verwenden
        public TimeServiceController(ITimeService timeService)
        {
            _timeService = timeService;
        }


        [HttpGet("Beispiel1")]
        public IActionResult GetTimeSerivce() //Konstruktor Injection (wird _timeService initialisiert) 
        {
            return Ok(_timeService.ShowTime());
        }

        //Beispiel 2: (Dienst nur in einer Methode) 
        [HttpGet("Beispiel2")] //Methoden - Injection
        public IActionResult GetTimeSerivce([FromServices] ITimeService explizieterTimeService)
        {
            return Ok(explizieterTimeService.ShowTime());
        }

        [HttpGet ("Beispiel3")]
        public IActionResult GetTimeService2()
        {
            //Verwenden der HttpContext Property
            ITimeService? timeService = this.HttpContext.RequestServices.GetService<ITimeService>();

            return Ok(timeService.ShowTime());
        }

    }
}

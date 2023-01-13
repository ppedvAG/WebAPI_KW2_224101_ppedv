using Microsoft.AspNetCore.Mvc;

namespace WebAPI_IOC_Samples.Controllers
{
    public class IchBinEinMVCController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Inventarios.Controllers
{
    public class SalidasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

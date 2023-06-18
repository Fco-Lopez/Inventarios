using Microsoft.AspNetCore.Mvc;

namespace Inventarios.Controllers
{
    public class EntradasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

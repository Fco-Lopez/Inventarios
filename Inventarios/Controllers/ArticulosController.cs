using Microsoft.AspNetCore.Mvc;

namespace Inventarios.Controllers
{
    public class ArticulosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

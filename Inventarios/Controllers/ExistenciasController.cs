using Inventarios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Inventarios.Controllers
{
    public class ExistenciasController : Controller
    {
        private readonly InventariosContext _context;

        public ExistenciasController(InventariosContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
           => View(await _context.Articulos.ToListAsync());
    }
}

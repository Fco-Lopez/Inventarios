using Inventarios.Models;
using Inventarios.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Inventarios.Controllers
{
    public class GruposController : Controller
    {
        private readonly InventariosContext _context;

        public GruposController(InventariosContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
           => View(await _context.Grupos.ToListAsync());

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(GrupoViewModel model)
        {
            if (ModelState.IsValid)
            {
                var grupo = new Grupo()
                {
                    Nombre = model.Nombre
                };
                _context.Add(grupo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Grupos != null)
            {
                var grupo = _context.Grupos.Find(id);
                if (grupo != null)
                {
                    var articulos = _context.Articulos
                        .Where(a => a.IdGrupo == grupo.IdGrupo)
                        .ToList();

                    if (articulos.Count > 0)
                    {
                        //TempData["Mensaje"] = "No se puede eliminar el grupo, ya existe un artículo que lo usa.";
                        return BadRequest("No se puede eliminar el grupo, ya existe un artículo que lo usa.");
                    }

                    _context.Grupos.Remove(grupo);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Modificar(int id)
           => View(_context.Grupos.Find(id));

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modificar(Grupo grupo)
        {
            if (ModelState.IsValid)
            {
                _context.Grupos.Update(grupo);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

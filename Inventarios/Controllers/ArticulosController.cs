using Inventarios.Models;
using Inventarios.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Inventarios.Controllers
{
    public class ArticulosController : Controller
    {
        private readonly InventariosContext _context;

        public ArticulosController(InventariosContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
           => View(await _context.Articulos.ToListAsync());

        public IActionResult Agregar()
        {
            ViewData["Grupos"] = new SelectList(_context.Grupos, "IdGrupo", "Nombre");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar(ArticuloViewModel model)
        {
            if (ModelState.IsValid)
            {
                //usamos LINQ para obtener
                bool existeCodigo = _context.Articulos.Any(a => a.Codigo == model.Codigo);
                if (!existeCodigo)
                {
                    var articulo = new Articulo()
                    {
                        Codigo = model.Codigo,
                        Nombre = model.Nombre,
                        Unidad = model.Unidad,
                        Existencia = model.Existencia,
                        Precio = model.Precio,
                        IdGrupo = model.IdGrupo,
                    };
                    _context.Add(articulo);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    return BadRequest("No se puede agregar otro artículo con el código " + model.Codigo + ".");
                }
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (_context.Articulos != null)
            {
                var articulo = _context.Articulos.Find(id);
                if (articulo != null)
                {
                    var entradas = _context.Entradas
                        .Where(a => a.IdArticulo == articulo.IdArticulo)
                        .ToList();

                    if (entradas.Count > 0)
                        return BadRequest("No se puede eliminar el artículo, ya existe una entrada que lo usa.");

                    var salidas = _context.Salidas
                        .Where(a => a.IdArticulo == articulo.IdArticulo)
                        .ToList();

                    if (salidas.Count > 0)
                        return BadRequest("No se puede eliminar el artículo, ya existe una salida que lo usa.");

                    _context.Articulos.Remove(articulo);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Modificar(int id)
        {
            ViewData["Grupos"] = new SelectList(_context.Grupos, "IdGrupo", "Nombre");
            var articulo = _context.Articulos.Find(id);
            if (articulo != null)
            {
                var articuloView = new ArticuloViewModel();
                articuloView.IdArticulo = articulo.IdArticulo;
                articuloView.Codigo = articulo.Codigo;
                articuloView.Nombre = articulo.Nombre;
                articuloView.Unidad = articulo.Unidad;
                articuloView.IdGrupo = articulo.IdGrupo;
                articuloView.Precio = articulo.Precio;
                return View(articuloView);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modificar(ArticuloViewModel model)
        {
            if (ModelState.IsValid)
            {
                var articulo = _context.Articulos.Find(model.IdArticulo);
                if (articulo != null)
                {
                    //articulo.Codigo = model.Codigo;
                    articulo.Nombre = model.Nombre;
                    articulo.Unidad = model.Unidad;
                    articulo.IdGrupo = model.IdGrupo;
                    articulo.Precio = model.Precio;
                    //_context.Articulos.Update(articulo);
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

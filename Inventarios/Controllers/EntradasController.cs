using Inventarios.Models.ViewModels;
using Inventarios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Inventarios.Controllers
{
    public class EntradasController : Controller
    {
        private readonly InventariosContext _context;

        public EntradasController(InventariosContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
           => View(await _context.Entradas.Include(e => e.IdArticuloNavigation).ToListAsync());

        public IActionResult Agregar()
        {
            ViewData["Articulos"] = new SelectList(_context.Articulos, "IdArticulo", "Nombre");            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar(EntradaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var entrada = new Entrada()
                {
                    Fecha = model.Fecha,
                    IdArticulo = model.IdArticulo,
                    Cantidad = model.Cantidad,
                    Precio = model.Precio,
                };
                _context.Add(entrada);
                //await _context.SaveChangesAsync();
                var articulo = _context.Articulos.Find(model.IdArticulo);
                if (articulo != null)
                {
                    articulo.Existencia += model.Cantidad;
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            if (_context.Entradas != null)
            {
                var entrada = _context.Entradas.Find(id);
                if (entrada != null)
                {                    
                    _context.Entradas.Remove(entrada);
                    var articulo = _context.Articulos.Find(entrada.IdArticulo);
                    if (articulo != null)
                    {
                        articulo.Existencia -= entrada.Cantidad;
                    }
                    await _context.SaveChangesAsync();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Modificar(int id)
        {
            ViewData["Articulos"] = new SelectList(_context.Articulos, "IdArticulo", "Nombre");
            //var entrada = _context.Entradas.Find(id);
            var entrada = _context.Entradas.Include(e => e.IdArticuloNavigation).FirstOrDefault(e => e.Id == id);
            if (entrada != null)
            {
                var entradaView = new EntradaViewModel();
                entradaView.Id = entrada.Id;
                entradaView.Fecha = entrada.Fecha;
                entradaView.IdArticulo = entrada.IdArticulo;
                entradaView.Codigo = entrada.IdArticuloNavigation.Codigo;
                entradaView.Cantidad = entrada.Cantidad;
                entradaView.Precio = entrada.Precio;
                return View(entradaView);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modificar(EntradaViewModel model)
        {
            if (ModelState.IsValid)
            {
                double DiferenciaCantidad = 0;
                var entrada = _context.Entradas.Find(model.Id);
                if (entrada != null)
                {
                    DiferenciaCantidad = model.Cantidad - entrada.Cantidad;
                    entrada.Fecha = model.Fecha;
                    entrada.IdArticulo = model.IdArticulo;
                    entrada.Cantidad = model.Cantidad;
                    entrada.Precio = model.Precio;
                    //await _context.SaveChangesAsync();
                    var articulo = _context.Articulos.Find(model.IdArticulo);
                    if (articulo != null)
                    {
                        articulo.Existencia += DiferenciaCantidad;
                        await _context.SaveChangesAsync();
                    }
                }                
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

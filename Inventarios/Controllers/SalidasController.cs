using Inventarios.Models.ViewModels;
using Inventarios.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Inventarios.Controllers
{
    public class SalidasController : Controller
    {
        private readonly InventariosContext _context;

        public SalidasController(InventariosContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
           => View(await _context.Salidas.Include(s => s.IdArticuloNavigation).ToListAsync());

        public IActionResult Agregar()
        {
            var Contextoarticulos = _context.Articulos;
            ViewData["Articulos"] = new SelectList(Contextoarticulos, "IdArticulo", "Nombre");
            var articulos = _context.Articulos
             .Select(a => new
             {
                 a.IdArticulo,
                 a.Codigo,
                 a.Precio
             })
             .ToList();

            ViewBag.DatosArticulo = articulos;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Agregar(SalidaViewModel model)
        {
            if (ModelState.IsValid)
            {
                var salida = new Salida()
                {
                    Fecha = model.Fecha,
                    IdArticulo = model.IdArticulo,
                    Cantidad = model.Cantidad,
                    Precio = model.Precio,
                };
                _context.Add(salida);
                //await _context.SaveChangesAsync();
                var articulo = _context.Articulos.Find(model.IdArticulo);
                if (articulo != null)
                {
                    articulo.Existencia -= model.Cantidad;
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
            if (_context.Salidas != null)
            {
                var salida = _context.Salidas.Find(id);
                if (salida != null)
                {
                    _context.Salidas.Remove(salida);
                    var articulo = _context.Articulos.Find(salida.IdArticulo);
                    if (articulo != null)
                    {
                        articulo.Existencia -= salida.Cantidad;
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
            var salida = _context.Salidas.Include(s => s.IdArticuloNavigation).FirstOrDefault(s => s.Id == id);
            if (salida != null)
            {
                var SalidaView = new SalidaViewModel();
                SalidaView.Id = salida.Id;
                SalidaView.Fecha = salida.Fecha;
                SalidaView.IdArticulo = salida.IdArticulo;
                SalidaView.Codigo = salida.IdArticuloNavigation.Codigo;
                SalidaView.Cantidad = salida.Cantidad;
                SalidaView.Precio = salida.Precio;
                return View(SalidaView);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Modificar(SalidaViewModel model)
        {
            if (ModelState.IsValid)
            {
                double DiferenciaCantidad = 0;
                var salida = _context.Salidas.Find(model.Id);
                if (salida != null)
                {
                    DiferenciaCantidad = model.Cantidad - salida.Cantidad;
                    salida.Fecha = model.Fecha;
                    salida.IdArticulo = model.IdArticulo;
                    salida.Cantidad = model.Cantidad;
                    salida.Precio = model.Precio;
                    //await _context.SaveChangesAsync();
                    var articulo = _context.Articulos.Find(model.IdArticulo);
                    if (articulo != null)
                    {
                        articulo.Existencia -= DiferenciaCantidad;
                        await _context.SaveChangesAsync();
                    }
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}

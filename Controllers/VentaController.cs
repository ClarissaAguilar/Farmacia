using Farmacia.AccesoADatos;
using Farmacia.EntidadesDeNegocio;
using Farmacia.LogicaDeNegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Farmacia.UI.AppWebAspCore.Controllers
{
    public class VentaController : Controller
    {
        VentaBL ventaBL = new VentaBL();
        // GET: VentaController
        public async Task<IActionResult> Index(Venta pVenta = null)
        {
            if (pVenta == null)
                pVenta = new Venta();
            if (pVenta.CodVenta == 0)
                pVenta.CodVenta = 10;
            else if (pVenta.CodVenta == -1)
                pVenta.CodVenta = 0;
            var ventas = await ventaBL.BuscarAsync(pVenta);
            ViewBag.Top = pVenta.CodVenta;
            return View(ventas);
        }

        // GET: VentaController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var venta = await ventaBL.ObtenerPorIdAsync(new Venta { IdVenta = id });
            return View(venta);
        }

        // GET: VentaController/Create
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // POST: VentaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Venta pVenta)
        {
            try
            {
                int result = await ventaBL.CrearAsync(pVenta);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pVenta);
            }
        }

        // GET: VentaController/Edit/5
        public async Task<IActionResult> Edit(Venta pVenta)
        {
            var venta = await ventaBL.ObtenerPorIdAsync(pVenta);
            ViewBag.Error = "";
            return View(venta);
        }

        // POST: VentaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Venta pVenta)
        {
            try
            {
                int result = await ventaBL.ModificarAsync(pVenta);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pVenta);
            }
        }

        // GET: VentaController/Delete/5
        public async Task<IActionResult> Delete(Venta pVenta)
        {
            ViewBag.Error = "";
            var venta = await ventaBL.ObtenerPorIdAsync(pVenta);
            return View(venta);
        }

        // POST: VentaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Venta pVenta)
        {
            try
            {
                int result = await ventaBL.EliminarAsync(pVenta);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pVenta);
            }
        }
    }
}

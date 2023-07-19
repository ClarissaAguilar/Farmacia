using Farmacia.AccesoADatos;
using Farmacia.EntidadesDeNegocio;
using Farmacia.LogicaDeNegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Farmacia.UI.AppWebAspCore.Controllers
{
    public class ReciboController : Controller
    {
        ReciboBL reciboBL = new ReciboBL();
        // GET: ReciboController
        public async Task<IActionResult> Index(Recibo pRecibo = null)
        {
            if (pRecibo == null)
                pRecibo = new Recibo();
            if (pRecibo.NoRecibo == 0)
                pRecibo.NoRecibo = 10;
            else if (pRecibo.NoRecibo == -1)
                pRecibo.NoRecibo = 0;
            var recibos = await reciboBL.BuscarAsync(pRecibo);
            ViewBag.Top = pRecibo.NoRecibo;
            return View(recibos);
        }

        // GET: ReciboController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var recibo = await reciboBL.ObtenerPorIdAsync(new Recibo { IdRecibo = id });
            return View(recibo);
        }

        // GET: ReciboController/Create
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // POST: ReciboController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Recibo pRecibo)
        {
            try
            {
                int result = await reciboBL.CrearAsync(pRecibo);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pRecibo);
            }
        }

        // GET: ReciboController/Edit/5
        public async Task<IActionResult> Edit(Recibo pRecibo)
        {
            var recibo = await reciboBL.ObtenerPorIdAsync(pRecibo);
            ViewBag.Error = "";
            return View(recibo);
        }

        // POST: ReciboController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Recibo pRecibo)
        {
            try
            {
                int result = await reciboBL.ModificarAsync(pRecibo);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pRecibo);
            }
        }

        // GET: ReciboController/Delete/5
        public async Task<IActionResult> Delete(Recibo pRecibo)
        {
            ViewBag.Error = "";
            var recibo = await reciboBL.ObtenerPorIdAsync(pRecibo);
            return View(recibo);
        }

        // POST: ReciboController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Recibo pRecibo)
        {
            try
            {
                int result = await reciboBL.EliminarAsync(pRecibo);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pRecibo);
            }
        }
    }
}

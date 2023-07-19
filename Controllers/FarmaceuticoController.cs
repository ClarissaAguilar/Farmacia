using Farmacia.AccesoADatos;
using Farmacia.EntidadesDeNegocio;
using Farmacia.LogicaDeNegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Farmacia.UI.AppWebAspCore.Controllers
{
    public class FarmaceuticoController : Controller
    {
        FarmaceuticoBL farmaceuticoBL = new FarmaceuticoBL();
        // GET: FarmaceuticoController
        public async Task<IActionResult> Index(Farmaceutico pFarmaceutico = null)
        {
            if (pFarmaceutico == null)
                pFarmaceutico = new Farmaceutico();
            if (pFarmaceutico.CodFarmaceutico == 0)
                pFarmaceutico.CodFarmaceutico = 10;
            else if (pFarmaceutico.CodFarmaceutico == -1)
                pFarmaceutico.CodFarmaceutico = 0;
            var farmaceuticos = await farmaceuticoBL.BuscarAsync(pFarmaceutico);
            ViewBag.Top = pFarmaceutico.CodFarmaceutico;
            return View(farmaceuticos);
        }

        // GET: FarmaceuticoController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var farmaceutico = await farmaceuticoBL.ObtenerPorIdAsync(new Farmaceutico { IdFarmaceutico = id });
            return View(farmaceutico);
        }

        // GET: FarmaceuticoController/Create
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // POST: FarmaceuticoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Farmaceutico pFarmaceutico)
        {
            try
            {
                int result = await farmaceuticoBL.CrearAsync(pFarmaceutico);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pFarmaceutico);
            }
        }

        // GET: FarmaceuticoController/Edit/5
        public async Task<IActionResult> Edit(Farmaceutico pFarmaceutico)
        {
            var farmaceutico = await farmaceuticoBL.ObtenerPorIdAsync(pFarmaceutico);
            ViewBag.Error = "";
            return View(farmaceutico);
        }

        // POST: FarmaceuticoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Farmaceutico pFarmaceutico)
        {
            try
            {
                int result = await farmaceuticoBL.ModificarAsync(pFarmaceutico);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pFarmaceutico);
            }
        }

        // GET: FarmaceuticoController/Delete/5
        public async Task<IActionResult> Delete(Farmaceutico pFarmaceutico)
        {
            ViewBag.Error = "";
            var farmaceutico = await farmaceuticoBL.ObtenerPorIdAsync(pFarmaceutico);
            return View(farmaceutico);
        }


        // POST: FarmaceuticoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Farmaceutico pFarmaceutico)
        {
            try
            {
                int result = await farmaceuticoBL.EliminarAsync(pFarmaceutico);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pFarmaceutico);
            }
        }
    }
}

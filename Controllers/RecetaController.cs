using Farmacia.AccesoADatos;
using Farmacia.EntidadesDeNegocio;
using Farmacia.LogicaDeNegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Farmacia.UI.AppWebAspCore.Controllers
{
    public class RecetaController : Controller
    {
        RecetaBL recetaBL = new RecetaBL();
        // GET: RecetaController
        public async Task<IActionResult> Index(Receta pReceta = null)
        {
            if (pReceta == null)
                pReceta = new Receta();
            if (pReceta.CodReceta == 0)
                pReceta.CodReceta = 10;
            else if (pReceta.CodReceta == -1)
                pReceta.CodReceta = 0;
            var recetas = await recetaBL.BuscarAsync(pReceta);
            ViewBag.Top = pReceta.CodReceta;
            return View(recetas);
        }

        // GET: RecetaController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var receta = await recetaBL.ObtenerPorIdAsync(new Receta { IdReceta = id });
            return View(receta);
        }

        // GET: RecetaController/Create
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // POST: RecetaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Receta pReceta)
        {
            try
            {
                int result = await recetaBL.CrearAsync(pReceta);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pReceta);
            }
        }

        // GET: RecetaController/Edit/5
        public async Task<IActionResult> Edit(Receta pReceta)
        {
            var receta = await recetaBL.ObtenerPorIdAsync(pReceta);
            ViewBag.Error = "";
            return View(receta);
        }

        // POST: RecetaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Receta pReceta)
        {
            try
            {
                int result = await recetaBL.ModificarAsync(pReceta);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pReceta);
            }
        }

        // GET: RecetaController/Delete/5
        public async Task<IActionResult> Delete(Receta pReceta)
        {
            ViewBag.Error = "";
            var receta = await recetaBL.ObtenerPorIdAsync(pReceta);
            return View(receta);
        }

        // POST: RecetaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Receta pReceta)
        {
            try
            {
                int result = await recetaBL.EliminarAsync(pReceta);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pReceta);
            }
        }
    }
}

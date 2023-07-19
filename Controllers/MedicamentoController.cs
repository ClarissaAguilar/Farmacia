using Farmacia.AccesoADatos;
using Farmacia.EntidadesDeNegocio;
using Farmacia.LogicaDeNegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Farmacia.UI.AppWebAspCore.Controllers
{
    public class MedicamentoController : Controller
    {
        MedicamentoBL medicamentoBL = new MedicamentoBL();
        // GET: MedicamentoController
        public async Task<IActionResult> Index(Medicamento pMedicamento = null)
        {
            if (pMedicamento == null)
                pMedicamento = new Medicamento();
            if (pMedicamento.CodMedicamento == 0)
                pMedicamento.CodMedicamento = 10;
            else if (pMedicamento.CodMedicamento == -1)
                pMedicamento.CodMedicamento = 0;
            var medicamentos = await medicamentoBL.BuscarAsync(pMedicamento);
            ViewBag.Top = pMedicamento.CodMedicamento;
            return View(medicamentos);
        }

        // GET: MedicamentoController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var medicamento = await medicamentoBL.ObtenerPorIdAsync(new Medicamento { IdMedicamento = id });
            return View(medicamento);
        }

        // GET: MedicamentoController/Create
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // POST: MedicamentoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Medicamento pMedicamento)
        {
            try
            {
                int result = await medicamentoBL.CrearAsync(pMedicamento);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pMedicamento);
            }
        }

        // GET: MedicamentoController/Edit/5
        public async Task<IActionResult> Edit(Medicamento pMedicamento)
        {
            var medicamento = await medicamentoBL.ObtenerPorIdAsync(pMedicamento);
            ViewBag.Error = "";
            return View(medicamento);
        }

        // POST: MedicamentoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Medicamento pMedicamento)
        {
            try
            {
                int result = await medicamentoBL.ModificarAsync(pMedicamento);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pMedicamento);
            }
        }

        // GET: MedicamentoController/Delete/5
        public async Task<IActionResult> Delete(Medicamento pMedicamento)
        {
            ViewBag.Error = "";
            var medicamento = await medicamentoBL.ObtenerPorIdAsync(pMedicamento);
            return View(medicamento);
        }

        // POST: MedicamentoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Medicamento pMedicamento)
        {
            try
            {
                int result = await medicamentoBL.EliminarAsync(pMedicamento);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pMedicamento);
            }
        }
    }
}

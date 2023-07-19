using Farmacia.AccesoADatos;
using Farmacia.EntidadesDeNegocio;
using Farmacia.LogicaDeNegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Farmacia.UI.AppWebAspCore.Controllers
{
    public class ClienteController : Controller
    {
        ClienteBL clienteBL = new ClienteBL();
        // GET: ClienteController
        public async Task<IActionResult> Index(Cliente pCliente = null)
        {
            if (pCliente == null)
                pCliente = new Cliente();
            if (pCliente.CodCliente == 0)
                pCliente.CodCliente = 10;
            else if (pCliente.CodCliente == -1)
                pCliente.CodCliente = 0;
            var clientes = await clienteBL.BuscarAsync(pCliente);
            ViewBag.Top = pCliente.CodCliente;
            return View(clientes);
        }

        // GET: ClienteController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var cliente = await clienteBL.ObtenerPorIdAsync(new Cliente { IdCliente = id });
            return View(cliente);
        }

        // GET: ClienteController/Create
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // POST: ClienteController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Cliente pCliente)
        {
            try
            {
                int result = await clienteBL.CrearAsync(pCliente);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pCliente);
            }
        }

        // GET: ClienteController/Edit/5
        public async Task<IActionResult> Edit(Cliente pCliente)
        {
            var cliente = await clienteBL.ObtenerPorIdAsync(pCliente);
            ViewBag.Error = "";
            return View(cliente);
        }

        // POST: ClienteController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Cliente pCliente)
        {
            try
            {
                int result = await clienteBL.ModificarAsync(pCliente);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pCliente);
            }
        }

        // GET: ClienteController/Delete/5
        public async Task<IActionResult> Delete(Cliente pCliente)
        {
            ViewBag.Error = "";
            var cliente = await clienteBL.ObtenerPorIdAsync(pCliente);
            return View(cliente);
        }

        // POST: ClienteController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Cliente pCliente)
        {
            try
            {
                int result = await clienteBL.EliminarAsync(pCliente);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pCliente);
            }
        }
    }
}

using Farmacia.AccesoADatos;
using Farmacia.EntidadesDeNegocio;
using Farmacia.LogicaDeNegocio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Farmacia.UI.AppWebAspCore.Controllers
{
    public class ProveedorController : Controller
    {
        ProveedorBL proveedorBL = new ProveedorBL();
        // GET: ProveedorController
        public async Task<IActionResult> Index(Proveedor pProveedor = null)
        {
            if (pProveedor == null)
                pProveedor = new Proveedor();
            if (pProveedor.CodProveedor == 0)
                pProveedor.CodProveedor = 10;
            else if (pProveedor.CodProveedor == -1)
                pProveedor.CodProveedor = 0;
            var proveedores = await proveedorBL.BuscarAsync(pProveedor);
            ViewBag.Top = pProveedor.CodProveedor;
            return View(proveedores);
        }

        // GET: ProveedorController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var proveedor = await proveedorBL.ObtenerPorIdAsync(new Proveedor { IdProveedor = id });
            return View(proveedor);
        }

        // GET: ProveedorController/Create
        public IActionResult Create()
        {
            ViewBag.Error = "";
            return View();
        }

        // POST: ProveedorController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Proveedor pProveedor)
        {
            try
            {
                int result = await proveedorBL.CrearAsync(pProveedor);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pProveedor);
            }
        }

        // GET: ProveedorController/Edit/5
        public async Task<IActionResult> Edit(Proveedor pProveedor)
        {
            var proveedor = await proveedorBL.ObtenerPorIdAsync(pProveedor);
            ViewBag.Error = "";
            return View(proveedor);
        }

        // POST: ProveedorController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Proveedor pProveedor)
        {
            try
            {
                int result = await proveedorBL.ModificarAsync(pProveedor);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pProveedor);
            }
        }

        // GET: ProveedorController/Delete/5
        public async Task<IActionResult> Delete(Proveedor pProveedor)
        {
            ViewBag.Error = "";
            var proveedor = await proveedorBL.ObtenerPorIdAsync(pProveedor);
            return View(proveedor);
        }

        // POST: ProveedorController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, Proveedor pProveedor)
        {
            try
            {
                int result = await proveedorBL.EliminarAsync(pProveedor);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View(pProveedor);
            }
        }
    }
}

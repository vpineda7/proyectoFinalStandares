using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZonaTecnologica.Controllers
{
    public class ProveedorController : Controller
    {
        basezonaDataContext DB = new basezonaDataContext();

        // GET: Proveedor
        public ActionResult Index(string message = "")
        {
            ViewBag.Message = message;
            List<VistaProveedore > Lista = (from c in DB.VistaProveedores
                                   select c).ToList();
            return View(Lista);
        }

        // GET: Proveedor/Details/5
        public ActionResult Details(int id)
        {
            var Modelo = DB.SP_BuscarProveedor(id).Single();
            return View(Modelo);
        }

        // GET: Proveedor/Create
        public ActionResult Create(string message = "")
        {
            ViewBag.Message = message;
            var Pro = DB.vProductos().ToList();
            ViewBag.Pro = Pro;
            return View();
        }

        // POST: Proveedor/Create
        [HttpPost]
        public ActionResult Create(VistaProveedore modelo)
        {
            try
            {
                var Modelo = DB.SP_AgregarProveedor(modelo.nombreProveedor, Convert.ToString(Session["UserName"])).SingleOrDefault();
                return RedirectToAction("Index", new { message = Modelo.mensaje });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Create", new { message = ex.Message });
            }
        }

        // GET: Proveedor/Edit/5
        public ActionResult Edit(int id, string message = "")
        {
            ViewBag.Message = message;
            var Modelo = DB.SP_BuscarProveedor(id).Single();
            return View(Modelo);
        }

        // POST: Proveedor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, SP_BuscarProveedorResult modelo)
        {
            try
            {
                var Modelo = DB.SP_ModificarProveedor(id, modelo.nombreProveedor, modelo.estado, Convert.ToString(Session["UserName"])).SingleOrDefault();
                if (Modelo.codigo == 0)
                {
                    return RedirectToAction("Edit", new { id = id, message = Modelo.mensaje });
                }

                return RedirectToAction("Index", new { message = Modelo.mensaje });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Edit", new { id = id, message = ex.Message });
            }
        }

        // GET: Proveedor/Delete/5
        public ActionResult Delete(int id, string message = "")
        {
            ViewBag.Message = message;
                var Modelo = DB.SP_BuscarProveedor(id).Single();
            return View(Modelo);
        }

        // POST: Proveedor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, SP_BuscarCompraResult modelo)
        {
            try
            {
                var Modelo = DB.SP_EliminarProveedor(id, Convert.ToString(Session["UserName"])).SingleOrDefault();
                if (Modelo.codigo == 0)
                {
                    return RedirectToAction("Delete", new { id = id, message = Modelo.mensaje });
                }

                return RedirectToAction("Index", new { message = Modelo.mensaje });
            }
            catch (Exception ex)
            {
                return RedirectToAction("Delete", new { id = id, message = ex.Message });
            }
        }
    }
}

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
            List<VistaProveedore> Lista = (from c in DB.VistaProveedores
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
        public ActionResult Create()
        {
            return View();
        }

        // POST: Proveedor/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Proveedor/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Proveedor/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Proveedor/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Proveedor/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

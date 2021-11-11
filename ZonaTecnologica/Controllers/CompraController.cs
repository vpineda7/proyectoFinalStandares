using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace ZonaTecnologica.Controllers
{
    public class CompraController : Controller
    {
        basezonaDataContext DB = new basezonaDataContext();

        // GET: Compra
        public ActionResult Index(string message = "")
        {
            ViewBag.Message = message;
            List<Vcompra> Lista = (from c in DB.Vcompras
                                    select c).ToList();
            return View(Lista);
        }

        // GET: Compra/Details/5
        public ActionResult Details(int id)
        {
            var Modelo = DB.SP_BuscarCompra(id).Single();
            return View(Modelo);

        }

        // GET: Compra/Create
        public ActionResult Create(string message = "")
        {
            ViewBag.Message = message;
            var Pros = DB.vProductos().ToList();
            ViewBag.Pros = Pros;
            var Prov = DB.vProveedores().ToList();
            ViewBag.Prov = Prov;
            return View();
        }

        // POST: Compra/Create
        [HttpPost]
        public ActionResult Create(Vcompra modelo)
        {
            try
            {
                // TODO: Add insert logic here
                var Modelo = DB.SP_AgregarCompra(modelo.cantidad, modelo.precio_unitario, modelo.id_producto,
                                   modelo.id_proveedor, Convert.ToString(Session["UserName"])).SingleOrDefault();               
                return RedirectToAction("Index", new { message = Modelo.mensaje });                
            }   
            catch (Exception ex)
            {
                return RedirectToAction("Create", new { message = ex.Message });
            }
        }

        // GET: Compra/Edit/5
        public ActionResult Edit(int id, string message = "")
        {
            ViewBag.Message = message;
            var Modelo = DB.SP_BuscarCompra(id).Single();
            var Pro = DB.vProductos().ToList();
            ViewBag.Pro = Pro;
            var Prov = DB.vProveedores().ToList();
            ViewBag.Prov = Prov;
            return View(Modelo);
        }

        // POST: Compra/Edit/5
        [HttpPost]
        public ActionResult Edit(int id,SP_BuscarCompraResult modelo)
        {
            try
            {
                // TODO: Add update logic 
                var Modelo = DB.SP_ModificarCompra(modelo.id_compra, modelo.cantidad, modelo.precioUnitario, modelo.id_producto, modelo.id_proveedor, modelo.estado, Convert.ToString(Session["UserName"])).SingleOrDefault();
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

        // GET: Compra/Delete/5
        public ActionResult Delete(int id, string message = "")
        {
            ViewBag.Message = message;
            var Modelo = DB.SP_BuscarCompra(id).Single();
            return View(Modelo);
        }

        // POST: Compra/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, SP_BuscarCompraResult modelo)
        {
            try
            {
                // TODO: Add delete logic here
                var Modelo = DB.SP_EliminarCompra(id, Convert.ToString(Session["UserName"])).SingleOrDefault();
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

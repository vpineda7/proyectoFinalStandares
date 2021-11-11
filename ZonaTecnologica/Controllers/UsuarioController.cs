using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data.SqlClient;

namespace ZonaTecnologica.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        basezonaDataContext BD = new basezonaDataContext();
        public ActionResult Index(string message="")
        {
            ViewBag.Message = message;
            List<Vusuario> Lista = (from c in BD.Vusuarios
                                    select c).ToList();
            return View(Lista);


        }


        public ActionResult RetrieveImage(int id)
        {
            byte[] cover = GetImageFromDataBase(id);
            if (cover != null)
            {
                return File(cover, "image/jpg");
            }
            else
            {
                return null;
            }
        }
        public byte[] GetImageFromDataBase(int Id)
        {

                var Modelo = (from c in BD.Vusuarios
                              where c.ID == Id
                              select c.foto.ToArray());
                byte[] cover = Modelo.First();
                return cover;
        }


        // GET: Usuario/Details/5
        public ActionResult Details(int id)
        {
            var Modelo = BD.SP_BuscarUsuario(id).Single();
            return View(Modelo);
        }

        // GET: Usuario/Create
        public ActionResult Create(string message = "")
        {
            ViewBag.Message = message;
            var Rol = BD.Vroles().ToList();
            ViewBag.Rol = Rol;
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        public ActionResult Create(Vusuario modelo, HttpPostedFileBase img)
        {
            try
            {
                if (img != null)
                {
                    var reader = new BinaryReader(img.InputStream);
                    modelo.foto = reader.ReadBytes(img.ContentLength);
                }
                var Modelo = BD.SP_AgregarUsuario(modelo.UserName,modelo.correo,modelo.primer_nombre,modelo.segundo_nombre,modelo.primer_apellido,modelo.segundo_apellido,modelo.id_rol,Convert.ToString(Session["UserName"]),modelo.foto).SingleOrDefault();
                if (Modelo.codigo == 0)
                {
                    return RedirectToAction("Create", new { message = Modelo.mensaje });
                }
                return RedirectToAction("Index",new { message=Modelo.mensaje});
            }
            catch (Exception ex)
            {
                return RedirectToAction("Create", new { message = ex.Message });
            }

        }

        // GET: Usuario/Edit/5
        // GET: Usuario/Edit/5
        public ActionResult Edit(int id, string message = "")
        {
            ViewBag.Message = message;
            var Modelo = BD.SP_BuscarUsuario(id).Single();
            var Rol = BD.Vroles().ToList();
            ViewBag.Rol = Rol;
            return View(Modelo);
        }

        // POST: Usuario/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, SP_BuscarUsuarioResult modelo, HttpPostedFileBase img)
        {
            try
            {
                // TODO: Add update logic here
                if (img != null)
                {
                    var reader = new BinaryReader(img.InputStream);
                    modelo.foto = reader.ReadBytes(img.ContentLength);
                }
                var Modelo = BD.SP_ModificarUsuario(modelo.ID, modelo.UserName, modelo.correo,modelo.primer_nombre,modelo.segundo_nombre,modelo.primer_apellido,modelo.segundo_apellido,modelo.vigencia_password,modelo.trylogin,modelo.foto,modelo.id_rol, Convert.ToString(Session["UserName"])).SingleOrDefault();
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

        // GET: Usuario/Delete/5
        public ActionResult Delete(int id, string message = "")
        {

            ViewBag.Message = message;
            var Modelo = BD.SP_BuscarUsuario(id).Single();
            return View(Modelo);
        }

        // POST: Usuario/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, SP_BuscarUsuarioResult modelo)
        {
            try
            {
                // TODO: Add delete logic here
                var Modelo = BD.SP_EliminarUsuario(id, Convert.ToString(Session["UserName"])).SingleOrDefault();
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

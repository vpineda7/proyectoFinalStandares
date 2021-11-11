using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZonaTecnologica.Controllers
{
    public class AdministradorController : Controller
    {
        // GET: Administrador
        basezonaDataContext BD = new basezonaDataContext();
        public ActionResult Index(string message = "")
        {
            ViewBag.Message = message;
            return View();
        }


        public ActionResult Edit(int id, string message = "")
        {

            ViewBag.Message = message;
            var Modelo = (from c in BD.Vusuarios
                          where c.ID == id
                          select c).Single();
            return View(Modelo);

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
    }
}

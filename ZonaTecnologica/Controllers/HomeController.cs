using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Web.Security;

namespace ZonaTecnologica.Controllers
{
    public class HomeController : Controller
    {
        basezonaDataContext BD = new basezonaDataContext();
        [AllowAnonymous]
        public ActionResult Index(string message = "")
        {
            ViewBag.Message = message;
            return View();
        }
        [AllowAnonymous]
        public ActionResult Index2(string message = "")
        {
            ViewBag.Message = message;
            return View();
        }
        [AllowAnonymous]
        public ActionResult Index3(string message = "")
        {
            ViewBag.Message = message;
            return View();
        }

        [AllowAnonymous]
        public ActionResult inicio()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }




        [HttpPost]
        public ActionResult Login(string usuario, string password, string rol)
        {
            if (password == "")
            {
                switch (rol)
                {
                    case "empleado":
                        return RedirectToAction("Index2", new { message = "Porfavor llene todos los campos" });

                    case "cliente":
                        return RedirectToAction("Index3", new { message = "Porfavor llene todos los campos" });

                    case "administrador":
                        return RedirectToAction("Index", new { message = "Porfavor llene todos los campos" });
                    default:
                        return View();
                }
            }
            else
            {
                var Modelo = BD.sp_validar_usuario(usuario, password, rol).Single();
                if (Modelo.id_usuario != -1)
                {
                    Session["email"] = Modelo.correo;

                    Session["UserName"] = usuario;
                    Session["Rol"] = rol;
                    Session["IdUsuario"] = Modelo.id_usuario;

                    if (Modelo.cod == -1)
                    {
                        switch (rol)
                        {
                            case "empleado":
                                FormsAuthentication.SetAuthCookie("empleado", true);
                                return RedirectToAction("Index", "Doctor", new { message = Modelo.mensaje });

                            case "cliente":
                                FormsAuthentication.SetAuthCookie("cliente", true);
                                return RedirectToAction("Index", "Paciente", new { message = Modelo.mensaje });

                            case "administrador":
                                FormsAuthentication.SetAuthCookie("administrador", true);
                                return RedirectToAction("Index", "Administrador", new { message = Modelo.mensaje });
                            default:
                                return View();
                        }
                    }
                    else
                    {
                        switch (rol)
                        {
                            case "empleado":
                                FormsAuthentication.SetAuthCookie("empleado", true);
                                return RedirectToAction("Index", "Doctor");

                            case "cliente":
                                FormsAuthentication.SetAuthCookie("cliente", true);
                                return RedirectToAction("Index", "Paciente");

                            case "administrador":
                                FormsAuthentication.SetAuthCookie("administrador", true);
                                return RedirectToAction("Index", "Administrador");

                            default:
                                return View();
                        }
                    }
                }
                else
                {
                    switch (rol)
                    {
                        case "empleado":
                            
                            return RedirectToAction("Index2", new { message = Modelo.mensaje });


                        case "cliente":
                            
                            return RedirectToAction("Index3", new { message = Modelo.mensaje });

                        case "administrador":
                            
                            return RedirectToAction("Index", new { message = Modelo.mensaje });
                        default:
                            return View();
                    }
                }
            }
        }


        public ActionResult Logout()
        {

            Session.Contents.RemoveAll();
            FormsAuthentication.SignOut();
            return RedirectToAction("inicio");
        }

        public ActionResult Opciones_Cuenta()
        {

            return View();
        }

    }
}
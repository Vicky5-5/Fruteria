
using Fruteria.ViewModels;
using LogicaBiblioteca.Contexto;
using LogicaBiblioteca.Modelos;
using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace Fruteria.Controllers
{
    public class LoginController : Controller
    {
       
        // GET: Login
        public ActionResult Login()
        {

            return View();
        }
        [ActionName("LogOut")]
        public ActionResult LogOut()
        {
            return RedirectToAction("Login", "Login");

        }
        //Revisar bien
        [HttpPost]
        public ActionResult Entrar(string email, string password, UsuariosViewModel entrar)
        {
            LoginManager loginManager = LoginManager.Instance;
            var usuario = loginManager.Login(email, password);
            if (usuario != null)
            {
                if (usuario.Administrador==true)
                {

                    HttpCookie cookie = new HttpCookie("Nombre", entrar.Nombre); // Para sustituir el Temp Data
                    var miCookie = ControllerContext.HttpContext.Request.Cookies["Nombre"];
                    if (miCookie != null)
                    {

                        var nombre = loginManager.GetCurrentUser();
                    }

                    return RedirectToAction("Index", "Productos");
                }
                else
                {
                    TempData["Usuario"] = entrar.Nombre; // Creamos un TempData para que cuando el usuario inicie sesión se vea su nombre
                    return RedirectToAction("Index", "Carrito");
                }

            }

            return RedirectToAction("Login", "Login");
        }

        [HttpGet, ActionName("VerUsuarios")]
        public ActionResult VerUsuarios()
        {

            return RedirectToAction("Index", "Usuarios");
        }
        public ActionResult Registro()
        {
            return View();
        }

        [HttpPost, ActionName("Registrar")]
        public ActionResult Registro(UsuariosViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var login = UsuariosViewModel.RegistroUsuarioNuevo(model.idUsuario, model.Nombre, model.Email, model.Password, model.Direccion);
                }
                catch (Exception ex)
                {

                    ViewBag.Error = $"Error al guardar: {ex.Message}";
                }
            }

            return RedirectToAction("Login", "Login");
        }


    }
}
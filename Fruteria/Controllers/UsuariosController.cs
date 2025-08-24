using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web.Configuration;
using System.Web.Http.Cors;
using System.Web.Mvc;
using Fruteria.ViewModels;
using LogicaBiblioteca.Managers;

namespace Fruteria.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsuariosController : Controller

    {

        // GET: Usuarios
        public ActionResult Index()
        {
            List<UsuariosViewModel> lista = new List<UsuariosViewModel>();


            lista = UsuariosViewModel.ListUsuarios();
            return View(lista);
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int id)
        {
            var prViewModel = UsuariosViewModel.DatosUnUsuario(id);

            if (prViewModel == null)
            {
                return HttpNotFound();
            }
            return View(prViewModel);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            return View();
        }

        public void EnviarCorreoBienvenida(string email)
        {

            try
            {
                //Guardamos en una variable la configuración que hay en el web config
                //Los datos de desde que correo se va a enviar los correos están en el web config debido a que a la larga será más fácil cambiar la configuración
                //Sin necesidad de entrar en el código
                var smtpSection = (SmtpSection)WebConfigurationManager.GetSection("system.net/mailSettings/smtp");
                //Configuramos el correo, su cuerpo y asunto
                var message = new MailMessage
                {
                    From = new MailAddress(smtpSection.Network.UserName),
                    Subject = "Bienvenido/a a Fruitis",
                    Body = "Gracias por ser el nuevo usuario de Fruitis, donde tienes frutas frescas y a buen precio. Tu frutería de confianza y de toda la vida"
                };
                //Se le añade el correo del usuario
                message.To.Add(email);
                //Se configura desde donde se va a enviar
                var smtpClient = new SmtpClient
                {
                    Host = smtpSection.Network.Host,
                    Port = smtpSection.Network.Port,
                    EnableSsl = smtpSection.Network.EnableSsl,
                    Credentials = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password)
                };
                //Se envia
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        public void EnviarCorreoEditado(string email)
        {

            try
            {
                //Guardamos en una variable la configuración que hay en el web config
                //Los datos de desde que correo se va a enviar los correos están en el web config debido a que a la larga será más fácil cambiar la configuración
                //Sin necesidad de entrar en el código
                var smtpSection = (SmtpSection)WebConfigurationManager.GetSection("system.net/mailSettings/smtp");
                //Configuramos el correo, su cuerpo y asunto
                var message = new MailMessage
                {
                    From = new MailAddress(smtpSection.Network.UserName),
                    Subject = "Cuenta Fruitis Editada",
                    Body = "Tu cuenta ha sido editada con éxito. Para más información contáctenos a info@fruitis.com. ¡Estaremos encantados de ayudarte!"
                };
                //Se le añade el correo del usuario
                message.To.Add(email);
                //Se configura desde donde se va a enviar
                var smtpClient = new SmtpClient
                {
                    Host = smtpSection.Network.Host,
                    Port = smtpSection.Network.Port,
                    EnableSsl = smtpSection.Network.EnableSsl,
                    Credentials = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password)
                };
                //Se envia
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
        public void EnviarCorreoBaja(string email)
        {

            try
            {
                //Guardamos en una variable la configuración que hay en el web config
                //Los datos de desde que correo se va a enviar los correos están en el web config debido a que a la larga será más fácil cambiar la configuración
                //Sin necesidad de entrar en el código
                var smtpSection = (SmtpSection)WebConfigurationManager.GetSection("system.net/mailSettings/smtp");
                //Configuramos el correo, su cuerpo y asunto
                var message = new MailMessage
                {
                    From = new MailAddress(smtpSection.Network.UserName),
                    Subject = "Dar de Baja",
                    Body = "Has sido dado de baja con éxito. Para más información contáctenos a info@fruitis.com. ¡Estaremos encantados de ayudarte!"
                };
                //Se le añade el correo del usuario
                message.To.Add(email);
                //Se configura desde donde se va a enviar
                var smtpClient = new SmtpClient
                {
                    Host = smtpSection.Network.Host,
                    Port = smtpSection.Network.Port,
                    EnableSsl = smtpSection.Network.EnableSsl,
                    Credentials = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password)
                };
                //Se envia
                smtpClient.Send(message);
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuariosViewModel usu)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var model = UsuariosViewModel.AddUsuario(usu.idUsuario, usu.Nombre, usu.Email, usu.Password, usu.Estado, usu.FechaRegistro, usu.FechaBaja, usu.Direccion, usu.Administrador);
                    return RedirectToAction("Index");

                    //EnviarCorreoBienvenida(model.Email);
                }
                catch (Exception ex)
                {

                    ViewBag.Error = $"Error al guardar: {ex.Message}";
                }
            }

            return View(usu);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int id)
        {
            UsuariosViewModel prViewModel = UsuariosViewModel.GetUsuario(id);

            if (prViewModel == null)
            {
                return HttpNotFound();
            }
            return View(prViewModel);
        }

        [HttpPost]
        public ActionResult Edit(UsuariosViewModel usuario)
        {
            if (ModelState.IsValid)
            {
                try
                {

                    var model = UsuariosViewModel.AddUsuario(usuario.idUsuario, usuario.Nombre, usuario.Email, usuario.Password, usuario.Estado, usuario.FechaRegistro, usuario.FechaBaja, usuario.Direccion, usuario.Administrador);

                    //Si el usuario se da de baja que envíe otro correo
                    if (model.Estado == false)
                    {
                       // EnviarCorreoBaja(model.Email);
                        return RedirectToAction("Index");

                    }
                    else
                    {
                        EnviarCorreoEditado(model.Email);
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Error = $"Error al guardar: {ex.Message}";
                }
            }

            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int id)
        {
            var prViewModel = UsuariosViewModel.GetUsuario(id);

            if (prViewModel == null)
            {
                return HttpNotFound();
            }
            return View(prViewModel);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    UsuariosViewModel model = UsuariosViewModel.RemoveUsuario(id);
                    return View("Index");

                }
                catch (Exception ex)
                {
                    ViewBag.Error = $"Error al guardar: {ex.Message}";

                }
            }
            return View("Index");
        }
        //para angular

        [HttpGet]
        public JsonResult ObtenerUsuarios()
        {
            var lista = UsuariosViewModel.ListUsuarios(); // Ya lo tienes en uso en Index
            return Json(lista, JsonRequestBehavior.AllowGet);
        }

    }

}

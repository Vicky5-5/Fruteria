using System;
using System.Web;
using Fruteria.ViewModels;
using LogicaBiblioteca.Managers;
using LogicaBiblioteca.Modelos;

public sealed class LoginManager
{
    private static LoginManager instance = null;
    private string currentUser;
    private bool bloqueo = false;
    private DateTime? tiempoBloqueo = null;
    private Usuario usuarioActual;

    private LoginManager()
    {
        // Constructor privado para evitar instanciación externa
    }
    //0123456789
    public static LoginManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LoginManager();
            }
            return instance;
        }
    }

    public Usuario Login(string email, string password)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
        {
            HttpContext.Current.Session["Fallido"] = "faltan datos por insertar"; // Reinicia intentos
        }

        if (bloqueo && tiempoBloqueo.HasValue && DateTime.Now < tiempoBloqueo.Value)
        {
            HttpContext.Current.Session["Mensaje"] = "Cuenta bloqueada. Inténtelo nuevamente después de 10 minutos.";
        }
        var login = UserManager.Login(email, password);
        var hash = UserManager.HashPassword(password);

        //Alamacenamos los intentos de inicio de sesión
        int intentosFallidos = (int)(HttpContext.Current.Session["IntentosFallidos"] ?? 0);
        if (login == null)
        {
            HttpContext.Current.Session["MensajeError"] = "Error. Usuario o contraseña incorrecto.";
            return null;
        }
        if (hash == login.Password && email == login.Email && login.Estado)
        {
            usuarioActual = login;
            currentUser = login.Nombre;
            HttpContext.Current.Session["UsuarioActual"] = login;
            HttpContext.Current.Session["Bienvenida"] = "Bienvenido/a:" + login.Nombre;
            HttpContext.Current.Session["IntentosFallidos"] = 0;
        }

        else
        {
            intentosFallidos++;
            HttpContext.Current.Session["Incorrecto"] = "La contraseña o el email están incorrectos. Inténtalo de nuevo.";
            HttpContext.Current.Session["IntentosFallidos"] = intentosFallidos;
        }

        if (intentosFallidos >= 3)
        {
            bloqueo = true;
            tiempoBloqueo = DateTime.Now.AddMinutes(10);
            HttpContext.Current.Session["Mensaje"] = "Se ha superado el número de intentos. Inténtelo de nuevo en 10 minutos.";
        }
        return login;
    }

    public Usuario GetCurrentUser()
    {
        return HttpContext.Current.Session["UsuarioActual"] as Usuario;
    }

    public void LogOut()
    {
        usuarioActual = null;
        currentUser = null;
        HttpContext.Current.Session["UsuarioActual"] = null;
    }

}

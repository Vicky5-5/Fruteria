using LogicaBiblioteca.Contexto;
using LogicaBiblioteca.Modelos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LogicaBiblioteca.Managers
{
    public class UserManager
    {
        #region Métodos
        //El método es static debido a no depende del modelo y no usa variables un con una variable exlícita (ejemplo: int = 4;)
        public static List<Usuario> ListarUsuarios()
        {
            using (var db = new FruteriaContext())
            {
                List<Usuario> usuarios = db.Usuario.ToList();
                return usuarios;
            }
        }

        public static Usuario ObtenerUsuario(int id)
        {
            using (FruteriaContext db = new FruteriaContext())
            {

                //Obtener el id del producto desde la base de datos
                Usuario usuario = db.Usuario.FirstOrDefault(a => a.idUsuario == id);
                return usuario;
            }
        }

        public static Usuario ObtenerDatosUnUsuario(int id)
        {
            using (var db = new FruteriaContext())
            {

                var usuario = db.Usuario.SingleOrDefault(p => p.idUsuario == id);

                return usuario;
            }
        }


        //Tener cuidado con lo que se pone en usuarios ya que se puede petar por los límites
        public static Usuario GuardarUsuario(int id, string nombre, string email, string password, bool estado, DateTime fechaRegistro, DateTime? fechaBaja, string direccion, bool administrador) //Esto sirve para editar y crear
        {
            using (var db = new FruteriaContext())
            {

                var usuario = db.Usuario.FirstOrDefault(a => a.idUsuario == id);
                var pEncript = HashPassword(password);
                //Productos productos = new Productos();
                //var producto = productos.ObtenerProducto(id);
                //Si el id es distinto de entramos en editar
                if (usuario != null)
                {
                    usuario.idUsuario = id;
                    usuario.Nombre = nombre;
                    usuario.Email = email;
                    usuario.Password = pEncript;
                    usuario.Estado = estado;
                    usuario.FechaBaja = fechaBaja;
                    usuario.Direccion = direccion;
                    usuario.Administrador = administrador;
                    db.SaveChanges();
                    return usuario;
                }

                //Esto es para crear un nuevo producto
                //Crear una prueba de error de, si existe el email, que lo eche para atrás la creación
                usuario = new Usuario()
                {

                    idUsuario = id,
                    Nombre = nombre,
                    Email = email,
                    Password = pEncript,
                    Estado = true,
                    FechaBaja = null,
                    Direccion = direccion,
                    Administrador = administrador
                };
                try
                {
                    db.Usuario.Add(usuario);

                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    throw new Exception(ex.Message);
                }
                return usuario;
            }
        }
        //APUNTES. CUANDO SE REGISTRE O SE CREE UN USUARIO QUE SE MANDE UN CORREO DE BIENVENIDA
        //       mailer.mailHost = "40.113.114.95";
        //mailer.mailPort = 587; // Gmail can use ports 25, 465 & 587; but must be 25 for medium trust environment.
        //mailer.mailSSL = false;
        //mailer.mailUsername = "relay@hosting.evotec.es";
        //mailer.mailPassword = "YNO#7Mda";

        public void EnviarConfirmacion()
        {
            //Instalar MailKit. Es un nugget
            //var mailMessage = new MimeMessage();
            //mailMessage.From.Add(new MailboxAddress("from name", "from email"));
            //mailMessage.To.Add(new MailboxAddress("to name", "to email"));
            //mailMessage.Subject = "subject";
            //mailMessage.Body = new TextPart("plain")
            //{
            //    Text = "Hello"
            //};

            //using (var smtpClient = new SmtpClient())
            //{
            //    smtpClient.Connect("smtp.gmail.com", 587, true);
            //    smtpClient.Authenticate("user", "password");
            //    smtpClient.Send(mailMessage);
            //    smtpClient.Disconnect(true);
            //}
            string to = "email";
            string from = "relay@hosting.evoec.es";
            MailMessage mensaje = new MailMessage(from, to);
            mensaje.Subject = "";
        }

        public static Usuario EliminarUsuario(int id)
        {
            using (var db = new FruteriaContext())
            {
                var usuario = db.Usuario.FirstOrDefault(a => a.idUsuario == id);
                var eliminado = db.Usuario.Remove(usuario);
                db.SaveChanges();
                return eliminado;
            }
        }


        public static Usuario Login(string email, string password)
        {
            using (var db = new FruteriaContext())
            {
                var pEncript = HashPassword(password);
                var usu2 = db.Usuario.ToList();
                var usu = db.Usuario.SingleOrDefault(uss => uss.Email == email && uss.Password == pEncript);
                return usu;
            }
        }

        public static string HashPassword(string password)
        {
            SHA256 hash = SHA256.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = hash.ComputeHash(encoding.GetBytes(password));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);

            return sb.ToString();

        }

        public static Usuario RegistrarUsuario(int id, string nombre, string email, string password, string direccion)
        {
            using (var db = new FruteriaContext())
            {
                Usuario usuario = new Usuario();

                usuario = db.Usuario.FirstOrDefault(a => a.idUsuario == id);
                var pEncript = HashPassword(password);

                usuario = new Usuario()
                {
                    idUsuario = id,
                    Nombre = nombre,
                    Email = email,
                    Password = pEncript,
                    Estado = true,
                    FechaBaja = null,
                    Direccion = direccion,
                    Administrador = false
                };

                try
                {
                    db.Usuario.Add(usuario);
                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    throw new Exception(ex.Message);
                }
                return usuario;

            }
        }



        #endregion



    }
}

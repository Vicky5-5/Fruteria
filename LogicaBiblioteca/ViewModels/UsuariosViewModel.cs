using LogicaBiblioteca.Managers;
using LogicaBiblioteca.Modelos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Fruteria.ViewModels
{
    public class UsuariosViewModel
    {
        public int idUsuario { get; set; }

        public string Nombre { get; set; }
        [DataType(DataType.EmailAddress)]

        public string Email { get; set; }

        [DataType(DataType.Password)]
        [StringLength(maximumLength: 30, MinimumLength = 10)]
        public string Password { get; set; }
        public bool Estado { get; set; }
        [DataType(DataType.Date)]

        public DateTime FechaRegistro { get; set; }

        [DataType(DataType.Date)]

        public DateTime? FechaBaja { get; set; }
        public string Direccion { get; set; }
        public bool Administrador { get; set; }

        public UsuariosViewModel(Usuario usuario)
        {
            this.idUsuario = usuario.idUsuario;
            this.Nombre = usuario.Nombre;
            this.Email = usuario.Email;
            this.Password = usuario.Password;
            this.Estado = usuario.Estado;
            this.FechaRegistro = usuario.FechaRegistro;
            this.FechaBaja = usuario.FechaBaja;
            this.Direccion = usuario.Direccion;
            this.Administrador = usuario.Administrador;
        }
        public UsuariosViewModel(int id, string nombre, string email, string password, string direccion)
        {
            this.idUsuario = id;
            this.Nombre = Nombre;
            this.Email = Email;
            this.Password = Password;
            this.Estado = true;
            this.FechaRegistro = DateTime.Now;
            this.FechaBaja = null;
            this.Administrador = false;
            this.Direccion = direccion;
        }
        public UsuariosViewModel()
        {
            
        }

        #region Métodos

        public static UsuariosViewModel GetUsuario(int id)
        {
            //Se guarda el producto de la base de datos, del objeto producto y se retorna el producto entero
            var model = UserManager.ObtenerUsuario(id);

            UsuariosViewModel usuario = new UsuariosViewModel(model);

            return usuario;
        }
        public static List<UsuariosViewModel> ListUsuarios()
        {

            var listar = UserManager.ListarUsuarios();
            List<UsuariosViewModel> lista = new List<UsuariosViewModel>();
            foreach (var item in listar)
            {
                UsuariosViewModel model = new UsuariosViewModel(item);

                lista.Add(model);

            }
            return lista;

        }

        public  static UsuariosViewModel DatosUnUsuario(int id)
        {

            var listar = UserManager.ObtenerDatosUnUsuario(id);

            UsuariosViewModel model = new UsuariosViewModel(listar);

            return model;

        }
        //Es usado por el modo editar y crear
        public static UsuariosViewModel AddUsuario(int id, string nombre, string email, string password, bool estado, DateTime fechaRegistro, DateTime? fechaBaja, string direccion, bool administrador)
        {
            var guardado = UserManager.GuardarUsuario(id, nombre, email, password, estado, fechaRegistro, fechaBaja, direccion, administrador);

            UsuariosViewModel model = new UsuariosViewModel(guardado);


            return model;
        }
        public static UsuariosViewModel RemoveUsuario(int id)
        {
            var borrado = UserManager.EliminarUsuario(id);

            UsuariosViewModel model = new UsuariosViewModel(borrado);
       
            return model;
        }
        public UsuariosViewModel LoginViewModel(string email, string password)
        {
          
            var login = UserManager.Login(email, password);
            UsuariosViewModel model = new UsuariosViewModel(login);
     
            return model;
        }

        public static UsuariosViewModel RegistroUsuarioNuevo(int id, string nombre, string email, string password, string direccion)
        {
            var registro = UserManager.RegistrarUsuario(id, nombre, email, password, direccion);
            UsuariosViewModel model = new UsuariosViewModel(id, nombre, email, password, direccion);

            return model;
        }
        #endregion
    }
}
using LogicaBiblioteca.Contexto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Web;

namespace LogicaBiblioteca.Modelos
{
    public class Usuario
    {
        #region Datos Usuario
        [Key]
        public int idUsuario { get; set; }

        [Required]
        public string Nombre { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(maximumLength: 4000, MinimumLength = 10)]
        public string Password { get; set; }
        public bool Estado { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaRegistro { get; private set; } // El Private para que solo sea de lectura y no se pueda editar

        [DataType(DataType.Date)]

        public DateTime? FechaBaja { get; set; }
        public string Direccion { get; set; }
        public bool Administrador { get; set; }

        public ICollection<Productos> Productos { get; set; }
        #endregion


        public Usuario()
        {
            FechaRegistro = DateTime.Now; // Asigna la fecha actual al crear el usuario
        }
    }



}
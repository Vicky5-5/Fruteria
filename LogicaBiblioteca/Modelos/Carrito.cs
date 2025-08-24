using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using LogicaBiblioteca.Contexto;
using LogicaBiblioteca.Logica;

namespace LogicaBiblioteca.Modelos
{
    public class Carrito
    {

        [Key]
        public Guid idCarrito { get; set; }

        [ForeignKey("oProducto")]
        public int idProducto { get; set; }

        [ForeignKey("oUsuario")]
        public int idUsuario { get; set; }

        public string NombreProducto { get; set; }
        public int CantidadCompra { get; set; } = 0;
        public decimal totalProductos { get; set; }

        public virtual Productos oProducto { get; set; }
        public virtual Usuario oUsuario { get; set; }

        public Carrito()
        {
            idCarrito = Guid.NewGuid(); // Nos aseguramos que cree un GUID nuevo
        }

    }
}
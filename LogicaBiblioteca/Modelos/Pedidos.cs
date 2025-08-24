using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogicaBiblioteca.Modelos
{
    public class Pedidos
    {
        [Key]
        public int idPedido { get; set; }

        // Clave foránea hacia Usuario
        public int idUsuario { get; set; }

        [ForeignKey("idUsuario")]
        public virtual Usuario oUsuario { get; set; }

        public DateTime FechaPedido { get; set; }

        // Clave foránea hacia Carrito
        public Guid idCarrito { get; set; }

        [ForeignKey("idCarrito")]
        public virtual Carrito oCarrito { get; set; }

        public DateTime FechaEstimadaEntrega { get; set; }

        public bool EstadoPedido { get; set; }

        public decimal Total { get; set; }

        public virtual ICollection<DetallesPedidos> DetallesPedidos { get; set; }
    }
}

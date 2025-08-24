using LogicaBiblioteca.Contexto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace LogicaBiblioteca.Modelos
{
    public class DetallesPedidos
    {
        [Key, Column(Order = 1)]
        public int idPedido { get; set; }
        [Key, Column(Order = 2)]
        public int idProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Oferta { get; set; }
        public decimal PrecioTotal { get; set; }

        [NotMapped] //Esto sirve para que no lo ponga en las migraciones de la base de datos
        public virtual Productos Productos { get; set; }

        public virtual Pedidos Pedidos { get; set; }

        

    }
}
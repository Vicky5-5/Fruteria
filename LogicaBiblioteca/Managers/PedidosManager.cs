using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaBiblioteca.Contexto;
using LogicaBiblioteca.Modelos;

namespace LogicaBiblioteca.Managers
{
    public class PedidosManager
    {
        public static DetallesPedidos Crear(int idPedido, int idProducto, int cantidad, decimal precio, decimal oferta)
        {
            return new DetallesPedidos
            {
                idPedido = idPedido,
                idProducto = idProducto,
                Cantidad = cantidad,
                Precio = precio,
                Oferta = oferta,
                PrecioTotal = (precio - oferta) * cantidad
            };
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogicaBiblioteca.Managers;
using LogicaBiblioteca.Modelos;

namespace LogicaBiblioteca.ViewModels
{
    public class PedidosViewModel
    {
        public int idPedido { get; set; }
        public int idUsuario { get; set; }
        public DateTime FechaPedido { get; set; }

        public DateTime FechaEstimadaEntrega { get; set; }
        public bool EstadoPedido { get; set; }
        public decimal Total { get; set; }
        public Productos oProductos { get; set; }
        public List<DetallesPedidos> DetallesPedidos { get; set; }

        public PedidosViewModel(Pedidos carrito)
        {
            this.idPedido = carrito.idPedido;
            this.idUsuario = carrito.idUsuario;
            this.FechaPedido = DateTime.Now;
            this.FechaEstimadaEntrega = DateTime.Now.AddDays(4);
            this.EstadoPedido = true;
            this.Total = carrito.Total;
        }
        public static PedidosViewModel Comprar(int id, int idUsuario, bool estadoPedido, decimal total, string nombreProductos)
        {
            var pedido = CarritoManager.ProcesarCompra(id, idUsuario, estadoPedido, total, nombreProductos);
            PedidosViewModel model = new PedidosViewModel(pedido);
            return model;
        }
    }
}

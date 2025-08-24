using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using LogicaBiblioteca.Modelos;

namespace Fruteria.ViewModels
{
    public class DetallesPedidosViewModel
    {
        public int idPedido { get; set; }
        public int idProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
        public decimal Oferta { get; set; }
        public decimal PrecioTotal { get; set; }

        //public List<DetallesPedidosViewModel> ListTodosProductos()
        //{

        //    var listar = DetallesPedidosManager.ListarDetallesPedidos();
        //    List<DetallesPedidosViewModel> lista = new List<DetallesPedidosViewModel>();
        //    foreach (var item in listar)
        //    {
        //        DetallesPedidosViewModel model = new DetallesPedidosViewModel();
        //        model.idPedido = item.idPedido;
        //        model.idProducto = item.idProducto;
        //        model.Cantidad = item.Cantidad;
        //        model.Precio = item.Precio;               
        //        model.Oferta = item.Oferta;
        //        model.PrecioTotal = item.PrecioTotal;

        //        lista.Add(model);

        //    }
        //    return lista;

        //}

    }
}
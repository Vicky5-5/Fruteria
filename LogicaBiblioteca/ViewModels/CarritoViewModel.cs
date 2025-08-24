using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using LogicaBiblioteca.Managers;
using LogicaBiblioteca.Modelos;

namespace Fruteria.ViewModels
{
    public class CarritoViewModel
    {
        public int idProducto { get; set; }
        public Guid idCarrito { get; set; }

        public string NombreProducto { get; set; }
        public int CantidadCompra { get; set; }
        public decimal totalProductos { get; set; } //Precio con la oferta
        public Guid idCarr { get; set; }
        //public decimal OfertaPr {  get; set; }
        public Productos oProducto { get; set; }
        public virtual Usuario oUsuario { get; set; }


        public CarritoViewModel(Carrito carro)
        {
            this.idCarrito = carro.idCarrito;
            this.oProducto = new Productos(); // Crear la instancia antes de usarla
            this.oProducto.idProducto = carro.idProducto;
            this.oProducto.NombreProducto = carro.NombreProducto;
            this.CantidadCompra = carro.CantidadCompra;
            this.totalProductos = carro.totalProductos;
            //this.idCarr = guardcarroado.idCarr;
        }
        public static List<ProductosViewModel> Catalogo()
        {
            var listar = ProductosManager.ListarProductos();
            List<ProductosViewModel> lista = new List<ProductosViewModel>();
            foreach (var item in listar)
            {
                ProductosViewModel model = new ProductosViewModel(item);

                lista.Add(model);

            }
            return lista;

        }
        public static List<CarritoViewModel> ListarCarritoPorUsuario(int idUsuario)
        {
            var listaViewModel = new List<CarritoViewModel>();

            var listaCarrito = CarritoManager.ObtenerCarritoPorUsuario(idUsuario); // Este método debe devolver una lista de objetos Carrito

            foreach (var item in listaCarrito)
            {
                CarritoViewModel model = new CarritoViewModel(item);
                listaViewModel.Add(model);
            }

            return listaViewModel;
        }
        public static Pedidos HacerCompra(int id, int idUsuario, bool estadoPedido, decimal total, string nombreProductos)
        {
            var pedido = CarritoManager.ProcesarCompra(id, idUsuario, estadoPedido, total, nombreProductos);
            return pedido;
        }
        public static CarritoViewModel AddProducto(int idProducto, string nombreP, int idUsuario, int cantidad)
        {

            var guardado = CarritoManager.AddCart(idProducto, nombreP, idUsuario, cantidad);
            CarritoViewModel model = new CarritoViewModel(guardado);

            return model;
        }
        public static void RemoveCart(int id)
        {
            CarritoManager.RemoveProductFromCart(id);
        }
    }
}
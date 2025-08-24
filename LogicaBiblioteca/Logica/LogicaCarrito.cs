using LogicaBiblioteca.Modelos;
using LogicaBiblioteca.Contexto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogicaBiblioteca.Logica
{
    public partial class LogicaCarrito
    {
        private FruteriaContext context = new FruteriaContext();
        Guid idCarr { get; set; }
        public const string CartSessionKey = "CartId";

        public static LogicaCarrito GetCarrito(HttpContextBase http)
        {
            var carr = new LogicaCarrito();
           // carr.idCarr = carr.GetCartId(http);
            return carr;
        }
        public void AddCart(Productos productos)
        {
            var producto = context.Carrito.SingleOrDefault(
                c => c.idCarrito == idCarr && c.idProducto == productos.idProducto);
            //Crear carro si éste no existe
            if (producto == null)
            {
                producto = new Carrito
                {
                    idProducto = productos.idProducto,
                    idCarrito = idCarr,
                    CantidadCompra = 1
                };
                //Añadir cosas al carrito
                context.Carrito.Add(producto);

            }
            else
            {
                //se añade uno o más
                producto.CantidadCompra++;
            }
            //Se guardan los cambios
            context.SaveChanges();
        }
        public int EliminarDelCarro(int id)
        {
            var producto = context.Carrito.SingleOrDefault(
               c => c.idCarrito == idCarr && c.idProducto == id);

            int contador = 0;

            if (producto != null)
            {
                if (producto.CantidadCompra > 1)
                {
                    producto.CantidadCompra--;
                    contador = producto.CantidadCompra;
                }
                else
                {
                    context.Carrito.Remove(producto);
                }
                context.SaveChanges();

            }
            return contador;
        }
        public void VaciarCarro()
        {
            var carro = context.Carrito.Where(cart => cart.idCarrito == idCarr);

            foreach (var cart in carro)
            {
                context.Carrito.Remove(cart);
            }
            context.SaveChanges();
        }
        public List<Carrito> GetCarritos()
        {
            return context.Carrito.Where(cart => cart.idCarrito == idCarr).ToList();
        }
        //Para añadir cosas más de una al carrito
        public int GetCount()
        {
            int? contador = (from Productos in context.Carrito where Productos.idCarrito == idCarr select (int?)Productos.CantidadCompra).Sum();
            return contador ?? 0;
        }
        //Ahora vamos con los precios de los productos
        public decimal ObtenerTotalProductos()
        {
            decimal? total = (from Productos in context.Carrito where Productos.idCarrito == idCarr select (int?)Productos.totalProductos).Sum();
            return total ?? decimal.Zero;
        }
        //Echar un ojo más tarde

        public int CrearPedido(DetallesPedidos detalle)
        {
            decimal totalPedido = 0; // Para acumular el total del pedido
            var items = GetCarritos(); // Obtener los productos del carrito

            // Crear instancia del pedido
            Pedidos nuevoPedido = new Pedidos
            {
                idUsuario = detalle.Pedidos.idUsuario, // Obtenemos el ID del Usuario
                FechaPedido = DateTime.Now,
                FechaEstimadaEntrega = DateTime.Now.AddDays(5), // Entrega en 5 días
                EstadoPedido = true,
                Total = 0, // Se calculará más adelante
                DetallesPedidos = new List<DetallesPedidos>() // Inicializar lista vacía
            };

            foreach (var item in items)
            {
                // Crear los detalles del pedido
                DetallesPedidos detalleP = new DetallesPedidos
                {
                    idProducto = item.idProducto,
                    Cantidad = item.CantidadCompra,
                    Precio = item.totalProductos, // Asumiendo que el precio está en `Productos`
                    PrecioTotal = item.CantidadCompra * item.totalProductos,
                    // Productos = item // Relación con la clase `Productos`
                };

                // Sumar al total del pedido
                totalPedido += detalleP.PrecioTotal;

                // Añadir el detalle al pedido
                nuevoPedido.DetallesPedidos.Add(detalleP);
            }

            // Actualizar el total en el pedido
            nuevoPedido.Total = totalPedido;

            // Guardar el pedido en la base de datos (asumiendo que el contexto tiene un DbSet<Pedidos>)
            context.Pedidos.Add(nuevoPedido);
            context.SaveChanges();

            // Retornar el ID del nuevo pedido
            return nuevoPedido.idPedido;
        }


        public string GetCartId(HttpContextBase context)
        {
            if (context.Session[CartSessionKey] == null)
            {
                if (!string.IsNullOrWhiteSpace(context.User.Identity.Name))
                {
                    context.Session[CartSessionKey] =
                        context.User.Identity.Name;
                }
                else
                {
                    // Generate a new random GUID using System.Guid class
                    Guid tempCartId = Guid.NewGuid();
                    // Send tempCartId back to client as a cookie
                    context.Session[CartSessionKey] = tempCartId.ToString();
                }
            }
            return context.Session[CartSessionKey].ToString();
        }
        //Esto es para identificar al usuario
        public void MigrateCart(string usuario)
        {
            var shoppingCart = context.Carrito.Where(
                c => c.idCarrito == idCarr);

            foreach (Carrito item in shoppingCart)
            {
               //item.idCarrito = usuario;
            }
            context.SaveChanges();
        }
    }
}

//https://learn.microsoft.com/es-es/aspnet/mvc/overview/older-versions/mvc-music-store/mvc-music-store-part-8
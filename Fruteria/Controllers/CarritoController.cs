using System.Collections.Generic;
using System.Web.Mvc;
using Fruteria.ViewModels;
using LogicaBiblioteca.Managers;
using LogicaBiblioteca.Modelos;

namespace Fruteria.Controllers
{

    public class CarritoController : Controller
    {
        public ActionResult Index()
        {
            var carrito = ProductosViewModel.ListProductos();
            return View(carrito);
        }

        [HttpGet]
        public ActionResult ListaCompras(int idProducto, int idUsuario, string nombreP, int cantidad = 1)
        {
            if (idProducto > 0 && idUsuario > 0 && cantidad > 0)
            {
                CarritoViewModel.AddProducto(idProducto, nombreP, idUsuario, cantidad);
                TempData["Mensaje"] = "Producto añadido al carrito.";
                return RedirectToAction("VerCarrito");
            }

            TempData["Error"] = "Datos inválidos para añadir al carrito.";
            return RedirectToAction("Index");
        }

        //[HttpPost]
        //public ActionResult VerCarrito(int idUsuario)
        //{
        //    var carrito = CarritoViewModel.ListarCarritoPorUsuario(idUsuario) ?? new List<CarritoViewModel>();
        //    return View(carrito);
        //}

        public ActionResult VerCarrito()
        {
            var usuario = LoginManager.Instance.GetCurrentUser();
            if (usuario == null)
            {
                TempData["Mensaje"] = "No hay usuario en sesión.";
                return RedirectToAction("Index", "Login");
            }

            var listaCarrito = CarritoViewModel.ListarCarritoPorUsuario(usuario.idUsuario)
                               ?? new List<CarritoViewModel>();

            return View(listaCarrito);
        }

        public ActionResult RemoveFromCart(int idProducto)
        {
            CarritoManager.RemoveProductFromCart(idProducto);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FinalizarCompra(int id, int idUsuario, bool estadoPedido, decimal total, string nombreProductos)
        {
            var pedido = CarritoViewModel.HacerCompra(id, idUsuario, estadoPedido, total, nombreProductos);
            TempData["Mensaje"] = pedido != null ? "Compra realizada con éxito." : "Hubo un problema al procesar la compra.";
            return RedirectToAction("VerCarrito", new { idUsuario });
        }

    }


}

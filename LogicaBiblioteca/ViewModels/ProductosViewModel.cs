using LogicaBiblioteca.Managers;
using LogicaBiblioteca.Modelos;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Fruteria.ViewModels
{
    public class ProductosViewModel
    {
        public int idProducto { get; set; }
        [DataType(DataType.ImageUrl)]
        public string ImagenURL { get; set; }
        public HttpPostedFileBase ImagenProductos { get; set; }
        public string NombreProducto { get; set; }

        public decimal Precio { get; set; }


        public int Stock { get; set; }


        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; } //Esto puede ser nulo aunque no tenga ?. Esto pasa con tipo String

        public Categoria Categoria { get; set; }


        public decimal? Oferta { get; set; }

        public string Origen { get; set; }


        public bool EnTemporada { get; set; }

        public List<ProductosViewModel> ListaProductos { get; set; }

        public ProductosViewModel (Productos productos)
        {
            this.idProducto = productos.idProducto;
            this.NombreProducto = productos.NombreProducto;
            this.Precio = productos.Precio;
            this.Stock = productos.Stock;
            this.Descripcion = productos.Descripcion;
            this.Categoria = (Categoria)productos.Categoria;
            this.EnTemporada = productos.EnTemporada;
            this.Origen = productos.Origen;
            this.Oferta = productos.Oferta;
            this.ImagenURL = productos.ImagenURL;
            this.ImagenProductos = productos.ImagenProductos;
        }

        public ProductosViewModel()
        {
        }
        #region MetodosLlamadaAProductos
        public static ProductosViewModel GetProducto(int id)
        {
            //Se guarda el producto de la base de datos, del objeto producto y se retorna el producto entero
            var nuevo = ProductosManager.ObtenerProducto(id);
            ProductosViewModel model = new ProductosViewModel(nuevo);

            return model;
        }
        //List
        public static List<ProductosViewModel> ListProductos()
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
        public static ProductosViewModel DatosUnProducto(int id)
        {

            var listar = ProductosManager.ObtenerDatosUnproducto(id);

            ProductosViewModel model = new ProductosViewModel(listar);
            return model;

        }

        public static ProductosViewModel AddProducto(int id, string nombre, decimal precio, int stock, string descripcion, Categoria categoria, string origen, decimal? oferta, bool temporada,string url, HttpPostedFileBase imagen)
        {
            var guardado = ProductosManager.GuardarProducto(id, nombre, precio, stock, descripcion, (LogicaBiblioteca.Modelos.Categoria)categoria, oferta, origen, temporada,url, imagen);

            ProductosViewModel model = new ProductosViewModel(guardado);        
            return model;
        }

        public static ProductosViewModel RemoveProducto(int id)
        {
            var borrado = ProductosManager.EliminarProducto(id);
            ProductosViewModel model = new ProductosViewModel(borrado);
            return model;
        }

        public static List<ProductosViewModel> ListaPorCategoría(Categoria categoria)
        {
            var listar = ProductosManager.VerPorCategoria((LogicaBiblioteca.Modelos.Categoria)categoria);
            List<ProductosViewModel> lista = new List<ProductosViewModel>();
            foreach (var item in listar)
            {
                ProductosViewModel model = new ProductosViewModel(item);
       
                lista.Add(model);

            }
            return lista;
        }
        #endregion
    }

    public enum Categoria
    {
        Tropical,

        EnTemporada,

        Nacional
    }
}
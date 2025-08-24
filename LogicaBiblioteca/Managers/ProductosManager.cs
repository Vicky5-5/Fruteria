using LogicaBiblioteca.Contexto;
using LogicaBiblioteca.Modelos;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace LogicaBiblioteca.Managers
{
    public class ProductosManager
    {
        #region Métodos
        public static Productos ObtenerProducto(int id)
        {
            using (FruteriaContext db = new FruteriaContext())
            {

                //Obtener el id del producto desde la base de datos
                var producto = db.Productos.FirstOrDefault(a => a.idProducto == id);
                return producto;
            }
        }
        public static Productos ObtenerDatosUnproducto(int id)
        {
            using (var db = new FruteriaContext())
            {

                var producto = db.Productos.SingleOrDefault(p => p.idProducto == id);
                return producto;
            }
        }
        public static List<Productos> ListarProductos()
        {
            using (var db = new FruteriaContext())
            {
                List<Productos> productos = db.Productos.ToList();
                return productos;
            }
        }

        public static Productos GuardarProducto(int id, string nombre, decimal precio, int stock, string descripcion, Categoria categoria, decimal? oferta, string origen, bool temporada, string url, HttpPostedFileBase imagen) //Esto sirve para editar y crear
        {
            using (var db = new FruteriaContext())
            {

                var producto = db.Productos.FirstOrDefault(a => a.idProducto == id);
                //Si el id es distinto de entramos en editar
                if (producto != null)
                {
                    producto.idProducto = id;
                    producto.NombreProducto = nombre;
                    producto.Precio = precio;
                    producto.Stock = stock;
                    producto.Descripcion = descripcion;
                    producto.Categoria = categoria;
                    producto.Oferta = oferta;
                    producto.Origen = origen;
                    producto.EnTemporada = temporada;
                    producto.ImagenURL = url;
                    producto.ImagenProductos = imagen;
                    db.SaveChanges();
                    return producto;
                }

                //Esto es para crear un nuevo producto
                producto = new Productos()
                {

                    idProducto = id,
                    NombreProducto = nombre,
                    Precio = precio,
                    Stock = stock,
                    Descripcion = descripcion,
                    Categoria = categoria,
                    Oferta = oferta,
                    Origen = origen,
                    EnTemporada = temporada,
                    ImagenURL = url,
                    ImagenProductos = imagen

                };

                try
                {
                    db.Productos.Add(producto);

                    db.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    throw new Exception(ex.Message);
                }

                return producto;
            }

        }

        public static Productos EliminarProducto(int id)
        {
            using (var db = new FruteriaContext())
            {
                var producto = db.Productos.FirstOrDefault(a => a.idProducto == id);
                var eliminado = db.Productos.Remove(producto);
                db.SaveChanges();
                return eliminado;
            }
        }

        public static List<Productos> VerPorCategoria(Categoria categoria)
        {
            using (var db = new FruteriaContext())
            {
                List<Productos> productos = new List<Productos>();

                var lista = db.Productos.Where(a => a.Categoria == categoria).ToList();
                db.SaveChanges();
                return lista;
            }
        }

        #endregion
    }
}

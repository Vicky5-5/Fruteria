using Fruteria.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Categoria = Fruteria.ViewModels.Categoria; //Para ver por categorias al pinchar en una imagen

namespace Fruteria.Controllers
{
    //QUITAR USING Y NO ACCEDER A LA BASE DE DATOS EN EL CONTROLLER BAJO NINGUN CONCEPTO
    public class ProductosController : Controller
    {

        public ActionResult Index()
        {
            List<ProductosViewModel> lista = new List<ProductosViewModel>();

            lista = ProductosViewModel.ListProductos();
            //lista.AddRange(viewModel.ListProductos()); //Esto es otra forma de hacer el listado
            return View(lista);
        }

        public ActionResult Details(int id)
        {
            
            var prViewModel = ProductosViewModel.DatosUnProducto(id);

            if (prViewModel == null)
            {
                return HttpNotFound();
            }
            return View(prViewModel);
        }

        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(ProductosViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = Path.GetFileNameWithoutExtension(model.ImagenProductos.FileName);
                    string extension = Path.GetExtension(model.ImagenProductos.FileName);
                    fileName = DateTime.Now.ToString("yymmssfff") + extension;
                    string relativo = "/Content/Imagenes/ImagenesProductos/" + fileName;
                    model.ImagenURL = relativo;
                    string absoluto = Path.Combine(Server.MapPath(relativo));
                    model = ProductosViewModel.AddProducto(model.idProducto, model.NombreProducto, model.Precio, model.Stock, model.Descripcion, model.Categoria, model.Origen, model.Oferta, model.EnTemporada, relativo, model.ImagenProductos);
                        return RedirectToAction("Index");
                    
                }
                catch (Exception ex)
                {

                    ViewBag.Error = $"Error al guardar: {ex.Message}";
                }
            }

            return View(model);


        }

        [HttpGet]
        public ActionResult Edit(int id)
        {

            //Llamar a la funcion del ViewModel para obetner el producto
            
            var prViewModel = ProductosViewModel.GetProducto(id);

            if (prViewModel == null)
            {
                return HttpNotFound();
            }
            return View(prViewModel);

        }
        [HttpPost]
        public ActionResult Edit(ProductosViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string fileName = Path.GetFileNameWithoutExtension(model.ImagenProductos.FileName);
                    string extension = Path.GetExtension(model.ImagenProductos.FileName);
                    fileName = DateTime.Now.ToString("yymmssfff") + extension;
                    string relativo = "/Content/Imagenes/ImagenesProductos/" + fileName;
                    model.ImagenURL = relativo;
                    string absoluto = Path.Combine(Server.MapPath(relativo));

                    model.ImagenProductos.SaveAs(absoluto);
                    model = ProductosViewModel.AddProducto(model.idProducto, model.NombreProducto, model.Precio, model.Stock, model.Descripcion, model.Categoria, model.Origen, model.Oferta, model.EnTemporada, relativo,model.ImagenProductos);
                
                return RedirectToAction("Index");
                    
                }
                catch (Exception ex)
                {
                    ViewBag.Error = $"Error al guardar: {ex.Message}";
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {

            //Llamar a la funcion del ViewModel para obetner el producto
            var prViewModel = ProductosViewModel.GetProducto(id);


            if (prViewModel == null)
            {
                return HttpNotFound();
            }
            return View(prViewModel);

        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ProductosViewModel model = ProductosViewModel.RemoveProducto(id);
                }
                catch (Exception ex)
                {
                    ViewBag.Error = $"Error al guardar: {ex.Message}";

                }
            }
            return View("Index");
        }

        [HttpGet, ActionName("VerCategorias")]
        public ActionResult VerCategorias(Categoria categoria)
        {
            List<ProductosViewModel> lista = new List<ProductosViewModel>();

            lista = ProductosViewModel.ListaPorCategoría(categoria);
            //lista.AddRange(viewModel.ListProductos()); //Esto es otra forma de hacer el listado
            return View(lista);
        }


    }
}

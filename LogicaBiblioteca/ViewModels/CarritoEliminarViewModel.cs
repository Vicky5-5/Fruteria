using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fruteria.ViewModels
{
    public class CarritoEliminarViewModel
    {
        public string mensaje {  get; set; }
        public decimal totalCar {  get; set; }
        public int contadorCarrito { get; set; }
        public int contadorProducto { get; set; }
        public int idBorrado { get; set; }
    }
}
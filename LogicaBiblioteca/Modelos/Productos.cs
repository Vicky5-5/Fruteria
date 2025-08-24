using System.Collections.Generic;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using LogicaBiblioteca.Contexto;
using System.Data.Entity.Validation;
using System.Web;
using System.Net;
using System.Security.Cryptography;
using System.Security.Policy;
using System.IO;
using System.Text;
using System.Web.UI.WebControls;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogicaBiblioteca.Modelos
{
    public class Productos
    {

        [Key]
        public int idProducto { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImagenURL { get; set; }
        [NotMapped]
        public HttpPostedFileBase ImagenProductos { get; set; }
        [Required]
        [DataType(DataType.Text)]
        public string NombreProducto { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]

        public decimal Precio { get; set; }
        [Required]

        public int Stock { get; set; }
        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; } //Esto puede ser nulo aunque no tenga ?. Esto pasa con tipo String

        public Categoria Categoria { get; set; }

        [DisplayFormat(DataFormatString = "{0:0.00}", ApplyFormatInEditMode = true)]
        [Range(0, 100)]
        public decimal? Oferta { get; set; }
        [Required]
        public string Origen { get; set; }
        public bool EnTemporada { get; set; }

    }

    public enum Categoria
    {
        [DescriptionAttribute("Tropical")]
        Tropical, //0

        [DescriptionAttribute("En Temporada")]
        EnTemporada, //1

        [DescriptionAttribute("Nacional")]
        Nacional //2
    }
}
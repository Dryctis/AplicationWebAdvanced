using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsModels
{
    public class clsCategorias
    {
        [Key]
        public int id { get; set; }
        [Required(ErrorMessage ="Ingrese un nombre para la categoria")]
        [Display(Name = "Nombre de categoria")]
        public string Nombre { get; set; }
        [Display(Name ="Orden de visualizacion")]
        [Range(1, 100, ErrorMessage = "El valor debe estar entre 1 y 100")]
        public int? Orden { get; set; }
    }
}

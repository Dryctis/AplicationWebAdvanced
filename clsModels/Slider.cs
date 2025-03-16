using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsModels
{
    public class Slider
    {
        [Key]
        public int SliderID { get; set; }

        [Required(ErrorMessage ="Ingrese un nombre para el slider")]
        [Display(Name = "Nombre del slider")]
        public string? nombre { get; set; }

        [Required]
        public bool estado { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Imagen")]

        public string? urlImagen { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace clsModels
{
    //Manera correcta de inicializar usando required
    //Muy importante IdentityUser e importar libreria Microsoft.AspNetCore.Identity
    public class AplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "El nombre es requerido")]
        public required string nombre { get; set; }

        [Required(ErrorMessage = "La direccion es obligatoria")]
        public required string direccion { get; set; }

        [Required(ErrorMessage = "La ciudad es obligatoria")]
        public required string ciudad { get; set; }

        [Required(ErrorMessage = "El pais es obligatorio")]
        public required string pais { get; set; }
    }
}

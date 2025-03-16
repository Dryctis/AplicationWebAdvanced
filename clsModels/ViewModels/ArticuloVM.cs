using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace clsModels.ViewModels
{
    public class ArticuloVM
    {
        public Articulo? Articulo { get; set; }
        public IEnumerable<SelectListItem>? ListaCategorias { get; set; }
    }
}

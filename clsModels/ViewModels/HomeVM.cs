using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//View model permite traer datos de varias tablas, no necesariamente deben estar relacionadas
namespace clsModels.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Slider> Sliders { get; set; }
        public IEnumerable<Articulo> ListaArticulos { get; set; }

        //Paginacion del inicio

        public int PageIndex { get; set; }
        public int TotalPage { get; set; }


        public HomeVM()
        {
            Sliders = new List<Slider>();
            ListaArticulos = new List<Articulo>();
        }
    }
}


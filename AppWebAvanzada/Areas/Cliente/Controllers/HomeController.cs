using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AppWebAvanzada.Models;
using clsCapaAcessoDatos.Data.Repository.IRepository;
using clsModels.ViewModels;
using clsModels;
using System.Drawing.Printing;

namespace AppWebAvanzada.Areas.Cliente.Controllers;

[Area("Cliente")]
public class HomeController : Controller
{
    
        private readonly IContenedorTrabajo _contenedorTrabajo;

    public HomeController(IContenedorTrabajo contenedorTrabajo)
    {
        _contenedorTrabajo = contenedorTrabajo;
    }

    //Primera version pagina de inicio sin paginacion
    //[HttpGet]
    //public IActionResult Index()
    //    {
    //        HomeVM homeVM = new HomeVM()
    //        {
    //            Sliders = _contenedorTrabajo.Slider.GetAll(),
    //            ListaArticulos = _contenedorTrabajo.Articulo.GetAll()
    //        };

    //    //Esta linea es para poder saber si estamos en el home o no
    //    ViewBag.IsHome = true;

    //    return View(homeVM);
    //    }

    //segunda version pagina de inicio con paginacion
    [HttpGet]
    public IActionResult Index(int page = 1, int PageSize = 6)
    {
        var articulos = _contenedorTrabajo.Articulo.AsQueryable();
        //paginar los resultados
        var PaginatedEntries = articulos.Skip((page - 1) * PageSize).Take(PageSize);
        HomeVM homeVM = new HomeVM()
        {
            Sliders = _contenedorTrabajo.Slider.GetAll(),
            ListaArticulos = PaginatedEntries.ToList(),
            PageIndex = page,
            TotalPage = (int)Math.Ceiling(articulos.Count() / (double)PageSize)
        };

        //Esta linea es para poder saber si estamos en el home o no
        ViewBag.IsHome = true;

        return View(homeVM);
    }

    //Para buscador
    [HttpGet]
    public IActionResult ResultadoBusqueda(string SearchString, int page = 1, int PageSize = 6)
    {
        var articulos = _contenedorTrabajo.Articulo.AsQueryable();

        //filtrar por titulo si hay un termino de busqueda
        if (!string.IsNullOrEmpty(SearchString))
        {
            articulos = articulos.Where(e => e.nombre.Contains(SearchString));
        }
        //paginar los resultados
        var PaginatedEntries = articulos.Skip((page - 1) * PageSize).Take(PageSize);

        //Crear el modelo para la vista
        var model = new ListaPaginada<Articulo>(PaginatedEntries.ToList(), articulos.Count(), page, PageSize, SearchString);
        return View(model);
    }

    [HttpGet]
        public IActionResult Detalle(int id)

        { var articuloDesdeBd = _contenedorTrabajo.Articulo.Get(id);

        return View(articuloDesdeBd);

        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }


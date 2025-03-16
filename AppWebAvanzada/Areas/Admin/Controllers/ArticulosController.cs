using clsCapaAcessoDatos.Data.Repository.IRepository;
using clsModels.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;

namespace BlogCore.Areas.Admin.Controllers
{
    [Authorize(Roles = ("Administrador"))]
    [Area("Admin")]
    public class ArticulosController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public ArticulosController(IContenedorTrabajo contenedorTrabajo, IWebHostEnvironment hostingEnvironment)
        {
            _contenedorTrabajo = contenedorTrabajo;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            ArticuloVM artiVM = new ArticuloVM()
            {
                Articulo = new clsModels.Articulo(),
                ListaCategorias = _contenedorTrabajo.Categoria.GetListCategorias()
            };

            return View(artiVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticuloVM artiVM)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;

                if (artiVM.Articulo.Id == 0 && archivos.Count > 0)
                {
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
                    var extension = Path.GetExtension(archivos[0].FileName);

                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }

                    artiVM.Articulo.urlImagen = @"\imagenes\articulos\" + nombreArchivo + extension;
                    artiVM.Articulo.fechaCreacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

                    _contenedorTrabajo.Articulo.Add(artiVM.Articulo);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Imagen", "Debes seleccionar una imagen");
                }
            }

            artiVM.ListaCategorias = _contenedorTrabajo.Categoria.GetListCategorias();
            return View(artiVM);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var articulo = _contenedorTrabajo.Articulo.GetFirstOrDefault(a => a.Id == id, includeProperties: "Categoria");
            if (articulo == null)
            {
                return NotFound();
            }

            ArticuloVM artiVM = new ArticuloVM()
            {
                Articulo = articulo,
                ListaCategorias = _contenedorTrabajo.Categoria.GetListCategorias()
            };

            return View(artiVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticuloVM artiVM)
        {
            if (ModelState.IsValid)
            {
                string rutaPrincipal = _hostingEnvironment.WebRootPath;
                var archivos = HttpContext.Request.Form.Files;
                var articuloDesdeDb = _contenedorTrabajo.Articulo.Get((int)artiVM.Articulo.Id);

                if (articuloDesdeDb == null)
                {
                    // Manejar el caso en que articuloDesdeDb es nulo
                    ModelState.AddModelError(string.Empty, "El artículo no existe.");
                    artiVM.ListaCategorias = _contenedorTrabajo.Categoria.GetListCategorias();
                    return View(artiVM);
                }

                if (archivos.Count() > 0)
                {
                    // Nueva imagen para el articulo
                    string nombreArchivo = Guid.NewGuid().ToString();
                    var subidas = Path.Combine(rutaPrincipal, @"imagenes\articulos");
                    var extension = Path.GetExtension(archivos[0].FileName);
                    var nuevaExtension = Path.GetExtension(archivos[0].FileName);

                    var rutaImagen = Path.Combine(rutaPrincipal, articuloDesdeDb.urlImagen?.TrimStart('\\') ?? string.Empty);

                    if (System.IO.File.Exists(rutaImagen))
                    {
                        System.IO.File.Delete(rutaImagen);
                    }

                    // Nuevamente subimos el archivo
                    using (var fileStreams = new FileStream(Path.Combine(subidas, nombreArchivo + extension), FileMode.Create))
                    {
                        archivos[0].CopyTo(fileStreams);
                    }
                    artiVM.Articulo.urlImagen = @"\imagenes\articulos\" + nombreArchivo + extension;
                    artiVM.Articulo.fechaCreacion = DateTime.Now.ToString();

                    _contenedorTrabajo.Articulo.Update(artiVM.Articulo);
                    _contenedorTrabajo.Save();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    // Aquí será cuando la imagen ya existe y se conserva
                    artiVM.Articulo.urlImagen = articuloDesdeDb.urlImagen;
                }
                _contenedorTrabajo.Articulo.Update(artiVM.Articulo);
                _contenedorTrabajo.Save();

                return RedirectToAction(nameof(Index));
            }

            artiVM.ListaCategorias = _contenedorTrabajo.Categoria.GetListCategorias();
            return View(artiVM);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var articuloDesdeBd = _contenedorTrabajo.Articulo.Get(id);
            if (articuloDesdeBd == null)
            {
                return Json(new { success = false, message = "Error borrando artículo" });
            }

            string rutaDirectorioPrincipal = _hostingEnvironment.WebRootPath;
            var rutaImagen = Path.Combine(rutaDirectorioPrincipal, articuloDesdeBd.urlImagen?.TrimStart('\\') ?? string.Empty);

            if (System.IO.File.Exists(rutaImagen))
            {
                System.IO.File.Delete(rutaImagen);
            }

            _contenedorTrabajo.Articulo.Remove(articuloDesdeBd);
            _contenedorTrabajo.Save();
            return Json(new { success = true, message = "Artículo borrado con éxito" });
        }



        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Articulo.GetAll(includeProperties: "Categoria") });
        }

        #endregion
    }
}

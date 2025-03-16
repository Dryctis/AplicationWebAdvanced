using clsCapaAcessoDatos.Data.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using clsModels;
using com.sun.xml.@internal.bind.v2.model.core;
using Microsoft.AspNetCore.Authorization;

namespace AppWebAvanzada.Areas.Admin.Controllers
{
    [Authorize(Roles =("Administrador"))]
    [Area("Admin")]
    public class CategoriasController : Controller
    {
        private readonly IContenedorTrabajo _contenedorTrabajo;

        public CategoriasController(IContenedorTrabajo contenedorTrabajo)
        {
            _contenedorTrabajo = contenedorTrabajo;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        //[AllowAnonymous] Este atributo permite que este metodo sea publico aunque en la clase se solicite este verificado como administrador
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(clsCategorias categoria)
        {
            if(ModelState.IsValid)
            {
                //Logica para guardar en BD
                _contenedorTrabajo.Categoria.Add(categoria);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));

            }


            return View(categoria);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            clsCategorias categoria = new clsCategorias();
            categoria = _contenedorTrabajo.Categoria.Get(id);
            if (categoria == null)
            {
                return NotFound();
            }


            return View(categoria);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(clsCategorias categoria)
        {
            if (ModelState.IsValid)
            {
                //Logica para actualizar en BD
                _contenedorTrabajo.Categoria.update(categoria);
                _contenedorTrabajo.Save();
                return RedirectToAction(nameof(Index));

            }


            return View(categoria);
        }


        #region  Llamadas a la API
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _contenedorTrabajo.Categoria.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objFromDb = _contenedorTrabajo.Categoria.Get(id);
            if (objFromDb == null)
            {
                return Json(new { succes = false, message = "Error borrando categoria" });
            }

            _contenedorTrabajo.Categoria.Remove(objFromDb);
            _contenedorTrabajo.Save();
            return Json(new { succes = true, message = "Categoria borrada con exito" });
        }

        #endregion
    }
}

using AppWebAvanzada.Data;
using clsCapaAcessoDatos.Data.Repository.IRepository;
using clsModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using sun.util.resources.cldr.de;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clsCapaAcessoDatos.Data.Repository
{
    class CategoriaRepository : clsRepository<clsCategorias>, ICategoriaRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoriaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> GetListCategorias()
        {
            return _db.Categoria.Select(i => new SelectListItem() 
            {
                Text = i.Nombre,
                Value = i.id.ToString()
            });
           
        }

        public void update(Articulo articulo)
        {
            var objDesdeDb = _db.Articulo.FirstOrDefault(a => a.Id == articulo.Id);
            if (objDesdeDb != null)
            {
                objDesdeDb.nombre = articulo.nombre;
                objDesdeDb.descripcion = articulo.descripcion;
                objDesdeDb.urlImagen = articulo.urlImagen;
                objDesdeDb.CategoriaId = articulo.CategoriaId;

                _db.SaveChanges();
            }
        }

        public void update(clsCategorias categoria)
        {
            var objDesdeDb = _db.Categoria.FirstOrDefault(c => c.id == categoria.id);
            if (objDesdeDb != null)
            {
                objDesdeDb.Nombre = categoria.Nombre;
                objDesdeDb.Orden = categoria.Orden;

                _db.SaveChanges();
            }
        }
    }
}

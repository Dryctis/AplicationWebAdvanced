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
    class SliderRepository : clsRepository<Slider>, ISliderRepository
    {
        private readonly ApplicationDbContext _db;

        public SliderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void update(Slider slider)
        {
            var objDesdeDb = _db.Slider.FirstOrDefault(a => a.SliderID == slider.SliderID);
            if (objDesdeDb != null)
            {
                objDesdeDb.nombre = slider.nombre;
                objDesdeDb.estado = slider.estado;
                objDesdeDb.urlImagen = slider.urlImagen;

                _db.SaveChanges();
            }
        }

        public void update(clsCategorias categoria)
        {
            throw new NotImplementedException();
        }
    }
}

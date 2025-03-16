using AppWebAvanzada.Data;
using clsCapaAcessoDatos.Data.Repository.IRepository;
using clsModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogCore.AccesoDatos.Data.Repository
{
    internal class ArticuloRepository : IRepository<Articulo>, IArticuloRepository
    {
        private readonly ApplicationDbContext _db;

        public ArticuloRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void Add(Articulo entity)
        {
            _db.Articulo.Add(entity);
            _db.SaveChanges();
        }

        public IQueryable<Articulo> AsQueryable()
        {
            return _db.Set<Articulo>().AsQueryable();
        }

        public Articulo Get(int id)
        {
            return _db.Articulo.Find(id);
        }

        public IEnumerable<Articulo> GetAll(Expression<Func<Articulo, bool>>? filter = null, Func<IQueryable<Articulo>, IOrderedQueryable<Articulo>>? orderBy = null, string? includeProperties = null)
        {
            IQueryable<Articulo> query = _db.Articulo;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        public Articulo GetFirstOrDefault(Expression<Func<Articulo, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<Articulo> query = _db.Articulo;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            return query.FirstOrDefault();
        }

        public void Remove(int id)
        {
            Articulo entity = _db.Articulo.Find(id);
            _db.Articulo.Remove(entity);
            _db.SaveChanges();
        }

        public void Remove(Articulo entity)
        {
            _db.Articulo.Remove(entity);
            _db.SaveChanges();
        }

        public void Update(Articulo articulo)
        {
            var objDesdeDb = _db.Articulo.FirstOrDefault(s => s.Id == articulo.Id);
            objDesdeDb.nombre = articulo.nombre;
            objDesdeDb.descripcion = articulo.descripcion;
            objDesdeDb.urlImagen = articulo.urlImagen;
            objDesdeDb.CategoriaId = articulo.CategoriaId;

            //_db.SaveChanges();
        }

        public void update(Articulo articulo)
        {
            var objDesdeDb = _db.Articulo.FirstOrDefault(s => s.Id == articulo.Id);
            objDesdeDb.nombre = articulo.nombre;
            objDesdeDb.descripcion = articulo.descripcion;
            objDesdeDb.urlImagen = articulo.urlImagen;
            objDesdeDb.CategoriaId = articulo.CategoriaId;

            //_db.SaveChanges();
        }
    }
}

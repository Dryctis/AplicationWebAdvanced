using AppWebAvanzada.Data;
using clsCapaAcessoDatos.Data.Repository.IRepository;
using clsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace clsCapaAcessoDatos.Data.Repository
{
    public class UsuarioRepository : clsRepository<AplicationUser>, IUsuarioRepository
    {
        private readonly ApplicationDbContext _db;

        public UsuarioRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Add(AplicationUser entity)
        {
            _db.AplicationUser.Add(entity);
            _db.SaveChanges();
        }

        public void BloquearUsuario(string IdUsuario)
        {
            var usuarioDesdeBd = _db.AplicationUser.FirstOrDefault(u => u.Id == IdUsuario);
            if (usuarioDesdeBd != null)
            {           
                usuarioDesdeBd.LockoutEnd = DateTime.Now.AddYears(1000);
                _db.SaveChanges();
            }
        }

        public void DesbloquearUsuario(string IdUsuario)
        {
            var usuarioDesdeBd = _db.AplicationUser.FirstOrDefault(u => u.Id == IdUsuario);
            usuarioDesdeBd.LockoutEnd = DateTime.Now;
            _db.SaveChanges();
            
        }

        public AplicationUser Get(int id)
        {
            return _db.AplicationUser.Find(id);
        }

        public IEnumerable<AplicationUser> GetAll(Expression<Func<AplicationUser, bool>>? filter = null, Func<IQueryable<AplicationUser>, IOrderedQueryable<AplicationUser>>? orderBy = null, string? includeProperties = null)
        {
            IQueryable<AplicationUser> query = _db.AplicationUser;

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

        public AplicationUser GetFirstOrDefault(Expression<Func<AplicationUser, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<AplicationUser> query = _db.AplicationUser;

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
            AplicationUser entityToRemove = _db.AplicationUser.Find(id);
            if (entityToRemove != null)
            {
                _db.AplicationUser.Remove(entityToRemove);
                _db.SaveChanges();
            }
        }

        public void Remove(AplicationUser entity)
        {
            _db.AplicationUser.Remove(entity);
            _db.SaveChanges();
        }
    }
}

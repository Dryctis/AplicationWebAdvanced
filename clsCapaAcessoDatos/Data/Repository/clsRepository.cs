﻿using clsCapaAcessoDatos.Data.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace clsCapaAcessoDatos.Data.Repository
{
    public class clsRepository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext Context;
        internal DbSet<T> dbSet;

        public clsRepository(DbContext context)
        {
            Context = context;
            this.dbSet = context.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public T Get(int id)
        {
           return dbSet.Find(id);
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter!= null)
            {
                query = query.Where(filter);
            }
            if (includeProperties != null)
            {
                foreach(var includeProperty in includeProperties.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }             
            }
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            IQueryable<T> query = dbSet;


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
            T entityToRemove = dbSet.Find();


        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}

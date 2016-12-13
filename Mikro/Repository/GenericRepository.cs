using Mikro.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace Mikro.Repository
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        private ApplicationDbContext _context = null;

        IDbSet<T> _objectSet;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _objectSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _objectSet.Add(entity);
        }

        public void Delete(T entity)
        {
            _objectSet.Remove(entity);
        }

        public T Select(Expression<Func<T, bool>> predicate)
        {
            return _objectSet.FirstOrDefault(predicate);
        }

        public T GetDetail(Expression<Func<T, bool>> predicate)
        {
            return _objectSet.First(predicate);
        }

        public IEnumerable<T> GetOverview(Func<T, bool> predicate = null)
        {
            if (predicate != null)
                return _objectSet.Where(predicate);
            return _objectSet.AsEnumerable();
        }

        public T GetPlus(Func<T, bool> predicate, Func<T, bool> predicate2)
        {
            return _objectSet.Where(predicate).Where(predicate).FirstOrDefault();              
        }
    }
}
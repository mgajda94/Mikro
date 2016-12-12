using Mikro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mikro.Repository
{
    public class UnitOfWork : IDisposable
    {
        private ApplicationDbContext _context = null;

        public UnitOfWork()
        {
            _context = new ApplicationDbContext();
        }

        public Dictionary<Type, object> repositories = new Dictionary<Type, object>();
        public IRepository<T> Repository<T>() where T : class
        {
            if (repositories.Keys.Contains(typeof(T)) == true)
                return repositories[typeof(T)] as IRepository<T>;

            IRepository<T> repo = new GenericRepository<T>(_context);
            repositories.Add(typeof(T), repo);
            return repo;
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                    _context.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
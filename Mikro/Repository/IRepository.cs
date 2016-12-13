using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mikro.Repository
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetOverview(Func<T, bool> predicate = null);
        T GetDetail(Expression<Func<T, bool>> predicate);
        T Select(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Delete(T entity);
        T GetPlus(Func<T, bool> predicate, Func<T, bool> predicate2);
    }
}

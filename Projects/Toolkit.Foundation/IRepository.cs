using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FoundationBase.Repository
{
    public interface IRepository<T> :IRepositoryBase, IDisposable where T : class
    {
        IQueryable<T> Fetch();
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Func<T, bool> predicate);
        T Single(Func<T, bool> predicate);
        T First(Func<T, bool> predicate);
        void Add(T entity);
        void Delete(T entity);
        void SaveChanges();
    }

}

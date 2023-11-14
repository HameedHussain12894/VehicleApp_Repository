using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Model;

namespace Vehicle.Dal.Infrastructure.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> GetOneRecord(Expression<Func<T, bool>> predicate);

        void Delete(T entity);
        void DeleteRange(IEnumerable<T> entities);

    }
}

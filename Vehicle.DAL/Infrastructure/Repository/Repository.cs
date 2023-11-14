using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Vehicle.Dal.Infrastructure.IRepository;

namespace Vehicle.Dal.Infrastructure.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDBContext _context;

        private readonly DbSet<T> dbset;

        public Repository(ApplicationDBContext context)
        {
            _context = context;
            dbset = _context.Set<T>();
        }
        public void Delete(T entity)
        {
           dbset.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            dbset.RemoveRange(entities);
        }

        public async Task<List<T>> GetAll()
        {
            return await dbset.ToListAsync();
        }

        public async Task<T> GetOneRecord(Expression<Func<T, bool>> predicate)
        {
            return await dbset.Where(predicate).FirstOrDefaultAsync();
        }

        
    }

}

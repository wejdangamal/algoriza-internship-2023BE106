using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Vzeeta.Core.Service;
using Vzeeta.Data;

namespace Vzeeta.Repository
{
    public class CustomService<T> : ICustomService<T> where T : class
    {
        private ApplicationDBContext _context;
        private DbSet<T> entity;

        public CustomService(ApplicationDBContext context)
        {
            _context = context;
            entity = _context.Set<T>();
        }
        public async Task<T> FindById(Expression<Func<T, bool>> match, string[] includes = null)
        {
            var query = entity.AsQueryable();
            if (includes != null)
            {
                foreach (var entity in includes)
                {
                    query = query.Include(entity);
                }
            }
            return await query.SingleOrDefaultAsync(match);
        }
    }
}

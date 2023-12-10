using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Vzeeta.Core.Model;
using Vzeeta.Core.Repository;
using Vzeeta.Data;
namespace Vzeeta.Repository
{
    public class Repository<T, D> : IRepository<T, D> where T :class 
    {
        private ApplicationDBContext _context;

        private DbSet<T> entities;

        public Repository(ApplicationDBContext _context)
        {
            this._context = _context;
            entities = _context.Set<T>();
        }

        public async Task<bool> Add(T entity)
        {
            if (entity is not null)
                // _context.Entry(entity).State = EntityState.Added;
                entities.Add(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> Delete(D id)
        {
            var entity = await entities.FindAsync(id);
            if (entity != null)
                entities.Remove(entity);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public IEnumerable<T> GetAll(int page, int pageSize, Func<T, bool> query, string[] includes = null)
        {
            IQueryable<T> entity = entities;
            page = page <= 0 ? 1 : page;

            pageSize = pageSize <= 0 ? 10 : pageSize;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    entity= entity.Include(include);
                }
            }
            var totalPages = (int)Math.Ceiling((decimal)entities.Count() / pageSize);
            var result = entity.Where(query).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return result;
        }


        public async Task<T> GetById(D id, string[]? includes)
        {
            return await entities.FindAsync(id);
        }

        public IEnumerable<T> GetAllEntities()
        {
            return entities.ToList();
        }

        public async Task<bool> Update(T entity)
        {
            if (entity is not null)
            {
                entities.Attach(entity);
            }
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}

using BookService.Interface;
using BookService.Model;
using Microsoft.EntityFrameworkCore;

namespace BookService.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BookEntityBase
    {
        private readonly DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        public void Delete(T entity)
        {
            entity.Deleted = true;
            _context.Set<T>().Update(entity);
        }

        public async Task<T?> FindAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<IQueryable<T>> GetAllAsync()
        {
            var results = await _context.Set<T>().ToListAsync();
            return results.AsQueryable();
        }

        public async Task Insert(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
        }
    }
}

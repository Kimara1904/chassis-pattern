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

        public Task<T?> FindAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

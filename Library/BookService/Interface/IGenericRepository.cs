using BookService.Model;

namespace BookService.Interface
{
    public interface IGenericRepository<T> where T : BookEntityBase
    {
        Task<IQueryable<T>> GetAllAsync();
        Task<T?> FindAsync(int id);
        Task Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}

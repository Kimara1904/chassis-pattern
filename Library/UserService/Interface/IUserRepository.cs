using UserService.Models;

namespace UserService.Interface
{
    public interface IUserRepository
    {
        Task<IQueryable<User>> GetAllAsync();
        Task<User?> FindAsync(int id);
        Task Insert(User user);
        void Update(User user);
        void Delete(User user);
        Task Save();
    }
}

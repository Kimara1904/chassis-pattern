using Microsoft.EntityFrameworkCore;
using UserService.Interface;
using UserService.Models;

namespace UserService.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _context;

        public UserRepository(DbContext context)
        {
            _context = context;
        }

        public void Delete(User user)
        {
            user.Deleted = true;
            _context.Set<User>().Update(user);
        }

        public async Task<User?> FindAsync(int id)
        {
            return await _context.Set<User>().FindAsync(id);
        }

        public async Task<IQueryable<User>> GetAllAsync()
        {
            var result = await _context.Set<User>().ToListAsync();
            return result.AsQueryable();
        }

        public async Task Insert(User user)
        {
            await _context.Set<User>().AddAsync(user);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(User user)
        {
            _context.Set<User>().Update(user);
        }
    }
}

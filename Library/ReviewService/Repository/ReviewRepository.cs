using Microsoft.EntityFrameworkCore;
using ReviewService.Interfaces;
using ReviewService.Model;

namespace ReviewService.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DbContext _context;

        public ReviewRepository(DbContext context)
        {
            _context = context;
        }

        public void Delete(Review review)
        {
            review.IsDeleted = true;
            _context.Set<Review>().Update(review);
        }

        public async Task<Review?> FindAsync(int id)
        {
            return await _context.Set<Review>().FindAsync(id);
        }

        public async Task<IQueryable<Review>> GetAllAsync()
        {
            var result = await _context.Set<Review>().ToListAsync();
            return result.AsQueryable();
        }

        public async Task Insert(Review review)
        {
            await _context.Set<Review>().AddAsync(review);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Update(Review review)
        {
            _context.Set<Review>().Update(review);
        }
    }
}

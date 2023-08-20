using ReviewService.Model;

namespace ReviewService.Interfaces
{
    public interface IReviewRepository
    {
        Task<IQueryable<Review>> GetAllAsync();
        Task<Review?> FindAsync(int id);
        Task Insert(Review review);
        void Update(Review review);
        void Delete(Review review);
        Task Save();
    }
}

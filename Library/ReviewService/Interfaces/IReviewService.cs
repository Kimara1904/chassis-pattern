using ReviewService.DTOs;

namespace ReviewService.Interfaces
{
    public interface IReviewService
    {
        Task CreateReview(string username, string email, CreateReviewDTO newReview);
        Task EditReview(int id, string username, EditReviewDTO newReviewInfo);
        Task<List<ReviewDTO>> GetVerifiedReviewsForBook(int bookId);
        Task<List<ReviewDTO>> GetUnverifiedReviewsForBook(int bookId);
        Task VerifyReview(int id, VerifyReviewDTO verifyReview);
    }
}

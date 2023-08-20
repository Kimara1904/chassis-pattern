using ReviewService.Enums;

namespace ReviewService.DTOs
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Comment { get; set; } = null!;
        public int Rate { get; set; }
        public ReviewVerifiedState Verified { get; set; }
    }
}

using ReviewService.Enums;

namespace ReviewService.Model
{
    public class Review
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Username { get; set; } = null!;
        public string Comment { get; set; } = null!;
        public int Rate { get; set; }
        public ReviewVerifiedState Verified { get; set; }
        public bool IsDeleted { get; set; }
    }
}

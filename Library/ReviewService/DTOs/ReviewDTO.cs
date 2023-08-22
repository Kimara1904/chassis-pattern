namespace ReviewService.DTOs
{
    public class ReviewDTO
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Username { get; set; } = null!;
        public string Comment { get; set; } = null!;
        public int Rate { get; set; }
        public string Verified { get; set; } = null!;
    }
}

namespace ReviewService.DTOs
{
    public class CreateReviewDTO
    {
        public int BookId { get; set; }
        public string Comment { get; set; } = null!;
        public int Rate { get; set; }
    }
}

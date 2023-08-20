namespace ReviewService.DTOs
{
    public class EditReviewDTO
    {
        public int BookId { get; set; }
        public string? Comment { get; set; }
        public int Rate { get; set; }
    }
}

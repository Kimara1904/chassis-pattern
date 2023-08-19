namespace BookService.DTO
{
    public class CreateBookDTO
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int Count { get; set; }
        public IFormFile? ImageFile { get; set; }
        public int AuthorId { get; set; }
    }
}

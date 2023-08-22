namespace BookService.DTO
{
    public class EditBookDTO
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int Count { get; set; }
        public IFormFile? ImageFile { get; set; }
        public int AuthorId { get; set; }
    }
}

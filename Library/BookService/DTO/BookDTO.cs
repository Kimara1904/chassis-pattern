namespace BookService.DTO
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int Count { get; set; }
        public byte[]? Image { get; set; }
        public AuthorDTO? Author { get; set; }
    }
}

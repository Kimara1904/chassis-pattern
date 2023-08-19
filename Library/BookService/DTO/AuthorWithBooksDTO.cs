namespace BookService.DTO
{
    public class AuthorWithBooksDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public List<AuthorsBookDTO> Books { get; set; } = null!;
    }
}

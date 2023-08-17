namespace BookService.Model
{
    public class Author
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int Count { get; set; }
        public virtual ICollection<Book> Books { get; set; } = null!;
        public bool Deleted { get; set; }
    }
}

namespace BookService.Model
{
    public class Author : BookEntityBase
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public virtual ICollection<Book> Books { get; set; } = null!;
    }
}

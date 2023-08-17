namespace BookService.Model
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int? AuthorId { get; set; }
        public virtual Author? Author { get; set; }
        public virtual ICollection<Rent> Rents { get; set; } = null!;
        public bool Deleted { get; set; }
    }
}

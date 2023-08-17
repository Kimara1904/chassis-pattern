namespace BookService.Model
{
    public class Book : BookEntityBase
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public int? AuthorId { get; set; }
        public virtual Author? Author { get; set; }
        public virtual ICollection<Rent> Rents { get; set; } = null!;
    }
}

namespace BookService.Model
{
    public class Rent : BookEntityBase
    {
        public int BookId { get; set; }
        public virtual Book Book { get; set; } = null!;
        public string Username { get; set; } = null!;
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}

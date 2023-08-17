namespace BookService.Model
{
    public class Rent
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; } = null!;
        public string Username { get; set; } = null!;
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}

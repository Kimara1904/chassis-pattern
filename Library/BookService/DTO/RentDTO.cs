namespace BookService.DTO
{
    public class RentDTO
    {
        public BookDTO Book { get; set; } = null!;
        public string Username { get; set; } = null!;
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public bool IsReturned { get; set; }
    }
}

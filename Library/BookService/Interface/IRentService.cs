using BookService.DTO;

namespace BookService.Interface
{
    public interface IRentService
    {
        Task<List<RentDTO>> GetAllRents();
        Task<List<RentDTO>> GetRentsByUsername(string username);
        Task<List<RentDTO>> GetUnReturnedUsersRents(string username);
        Task<List<RentDTO>> GetRentsByBookId(int bookId);
        Task RentBook(RentBookDTO newRent);
        Task ReturnBook(int bookId, string username);
    }
}

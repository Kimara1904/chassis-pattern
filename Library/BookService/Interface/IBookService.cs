using BookService.DTO;

namespace BookService.Interface
{
    public interface IBookService
    {
        Task<List<BookDTO>> GetAllBooks();
        Task<BookDTO> GetBook(int id);
        Task<BookDTO> CreateBook(CreateBookDTO newBook);
        Task<BookDTO> UpdateBook(EditBookDTO newBookInfo);
        Task DeleteBook(int id);
    }
}

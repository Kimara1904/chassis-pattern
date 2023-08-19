using BookService.DTO;

namespace BookService.Interface
{
    public interface IBookService
    {
        Task<List<BookDTO>> GetAllBooks();
        Task<AuthorWithBooksDTO> GetBooksByAuthorsId(int authorsId);
        Task<BookDTO> GetBook(int id);
        Task<BookDTO> CreateBook(CreateBookDTO newBook);
        Task<BookDTO> UpdateBook(int id, EditBookDTO newBookInfo);
        Task DeleteBook(int id);
    }
}

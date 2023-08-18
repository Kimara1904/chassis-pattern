using BookService.DTO;

namespace BookService.Interface
{
    public interface IAuthorService
    {
        Task<List<AuthorDTO>> GetAllAuthors();
        Task<AuthorDTO> GetAuthor(int id);
        Task<AuthorDTO> CreateAuthor(CreateAuthorDTO newBook);
        Task<AuthorDTO> UpdateAuthor(EditAuthorDTO newBookInfo);
        Task DeleteAuthor(int id);
    }
}

using AutoMapper;
using BookService.DTO;
using BookService.Interface;
using BookService.Model;
using Exceptions.Exeptions;
using Microsoft.EntityFrameworkCore;

namespace BookService.Services
{
    public class BookService : IBookService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;
        public BookService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<BookDTO> CreateBook(CreateBookDTO newBook)
        {
            var book = _mapper.Map<Book>(newBook);

            if (newBook.AuthorId > 0)
            {
                var author = await _repository._authorRepository.FindAsync(newBook.AuthorId) ??
                    throw new NotFoundException(string.Format("There is no author with id: {0}", newBook.AuthorId));

                book.AuthorId = author.Id;
            }

            if (newBook.ImageFile != null)
            {
                using var ms = new MemoryStream();
                newBook.ImageFile.CopyTo(ms);
                var fileBytes = ms.ToArray();

                book.Image = fileBytes;
            }

            await _repository._bookRepository.Insert(book);
            await _repository.SaveChanges();

            return _mapper.Map<BookDTO>(book);
        }

        public async Task DeleteBook(int id)
        {
            var book = await _repository._bookRepository.FindAsync(id) ??
                throw new NotFoundException(string.Format("There is no book with id: {0}", id));

            _repository._bookRepository.Delete(book);
            await _repository.SaveChanges();
        }

        public async Task<List<BookDTO>> GetAllBooks()
        {
            var bookQuery = await _repository._bookRepository.GetAllAsync();
            var books = bookQuery.Include(b => b.Author).ToList();

            return _mapper.Map<List<BookDTO>>(books);
        }

        public async Task<BookDTO> GetBook(int id)
        {
            var bookQuery = await _repository._bookRepository.GetAllAsync();
            var book = bookQuery.Include(b => b.Author).Where(b => b.Id == id).FirstOrDefault() ??
               throw new NotFoundException(string.Format("There is no book with id: {0}", id));

            return _mapper.Map<BookDTO>(book);
        }

        public async Task<AuthorWithBooksDTO> GetBooksByAuthorsId(int authorsId)
        {
            var authorQuery = await _repository._authorRepository.GetAllAsync();
            var author = authorQuery.Include(a => a.Books).Where(a => a.Id == authorsId).FirstOrDefault()
               ?? throw new NotFoundException(string.Format("There is no author with id: {0}", authorsId));

            return _mapper.Map<AuthorWithBooksDTO>(author);
        }

        public async Task<BookDTO> UpdateBook(int id, EditBookDTO newBookInfo)
        {
            var book = await _repository._bookRepository.FindAsync(id) ??
                throw new NotFoundException(string.Format("There is no book with id: {0}", id));

            if (newBookInfo.AuthorId > 0)
            {
                var author = await _repository._authorRepository.FindAsync(newBookInfo.AuthorId) ??
                    throw new NotFoundException(string.Format("There is no author with id: {0}", newBookInfo.AuthorId));

                book.AuthorId = author.Id;
                book.Author = author;
            }

            _mapper.Map<EditBookDTO, Book>(newBookInfo, book);

            _repository._bookRepository.Update(book);
            await _repository.SaveChanges();

            return _mapper.Map<BookDTO>(book);
        }
    }
}

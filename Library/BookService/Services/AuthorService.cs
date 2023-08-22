using AutoMapper;
using BookService.DTO;
using BookService.Interface;
using BookService.Model;
using Exceptions.Exeptions;
using Microsoft.EntityFrameworkCore;

namespace BookService.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public AuthorService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AuthorDTO> CreateAuthor(CreateAuthorDTO newBook)
        {
            var author = _mapper.Map<Author>(newBook);

            await _repository._authorRepository.Insert(author);
            await _repository.SaveChanges();

            return _mapper.Map<AuthorDTO>(author);
        }

        public async Task DeleteAuthor(int id)
        {
            var authorQuery = await _repository._authorRepository.GetAllAsync();
            var author = authorQuery.Include(a => a.Books).Where(a => a.Id == id).FirstOrDefault();

            if (author == null)
            {
                return;
            }

            _repository._authorRepository.Delete(author);
            await _repository.SaveChanges();
        }

        public async Task<List<AuthorDTO>> GetAllAuthors()
        {
            var authorQuery = await _repository._authorRepository.GetAllAsync();
            var authors = authorQuery.ToList();

            return _mapper.Map<List<AuthorDTO>>(authors);
        }

        public async Task<AuthorDTO> GetAuthor(int id)
        {
            var author = await _repository._authorRepository.FindAsync(id) ??
                throw new NotFoundException(string.Format("There is no author with id: {0}", id));

            return _mapper.Map<AuthorDTO>(author);
        }

        public async Task<AuthorDTO> UpdateAuthor(int id, EditAuthorDTO newBookInfo)
        {
            var author = await _repository._authorRepository.FindAsync(id) ??
                throw new NotFoundException(string.Format("There is no author with id: {0}", id));

            _mapper.Map<EditAuthorDTO, Author>(newBookInfo, author);

            _repository._authorRepository.Update(author);
            await _repository.SaveChanges();

            return _mapper.Map<AuthorDTO>(author);
        }
    }
}

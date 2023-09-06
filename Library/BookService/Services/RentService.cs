using AutoMapper;
using BookService.DTO;
using BookService.Interface;
using BookService.Model;
using Exceptions.Exeptions;
using Microsoft.EntityFrameworkCore;

namespace BookService.Services
{
    public class RentService : IRentService
    {
        private readonly IRepositoryWrapper _repository;
        private readonly IMapper _mapper;

        public RentService(IRepositoryWrapper repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<RentDTO>> GetAllRents()
        {
            var rentQurary = await _repository._rentRepository.GetAllAsync();
            var rents = rentQurary.Include(r => r.Book).ToList();

            return _mapper.Map<List<RentDTO>>(rents);
        }

        public async Task<List<RentDTO>> GetRentsByBookId(int bookId)
        {
            var rentQurary = await _repository._rentRepository.GetAllAsync();
            var rents = rentQurary.Include(r => r.Book).ThenInclude(b => b.Author).Where(r => r.BookId == bookId).ToList();

            return _mapper.Map<List<RentDTO>>(rents);
        }

        public async Task<List<RentDTO>> GetRentsByUsername(string username)
        {
            var rentQurary = await _repository._rentRepository.GetAllAsync();
            var rents = rentQurary.Include(r => r.Book).Where(r => r.Username.Equals(username)).ToList();

            return _mapper.Map<List<RentDTO>>(rents);
        }

        public async Task<List<RentDTO>> GetUnReturnedUsersRents(string username)
        {
            var rentQurary = await _repository._rentRepository.GetAllAsync();
            var rents = rentQurary.Include(r => r.Book).Where(r => r.Username.Equals(username) && !r.IsReturned).ToList();

            return _mapper.Map<List<RentDTO>>(rents);
        }

        public async Task RentBook(string username, RentBookDTO newRent)
        {
            var bookQuery = await _repository._bookRepository.GetAllAsync();
            var book = bookQuery.Where(b => b.Id == newRent.BookId && b.Count > 0).FirstOrDefault()
                ?? throw new NotFoundException(string.Format("There is not available book with id: {0}", newRent.BookId));
            var rent = _mapper.Map<Rent>(newRent);

            rent.Username = username;
            rent.RentDate = DateTime.Now;
            book.Count--;

            _repository._bookRepository.Update(book);
            await _repository._rentRepository.Insert(rent);
            await _repository.SaveChanges();
        }

        public async Task ReturnBook(int bookId, string username)
        {
            var rentQurary = await _repository._rentRepository.GetAllAsync();
            var rent = rentQurary.Where(r => r.Username.Equals(username) && r.BookId == bookId && !r.IsReturned).FirstOrDefault()
                ?? throw new NotFoundException(string.Format("User {0} didn't order book with id {1}", username, bookId));

            var book = await _repository._bookRepository.FindAsync(bookId);

            rent.ReturnDate = DateTime.Now;
            rent.IsReturned = true;

            if (book != null)
            {
                book.Count++;
                _repository._bookRepository.Update(book);
            }

            _repository._rentRepository.Update(rent);
            await _repository.SaveChanges();
        }
    }
}

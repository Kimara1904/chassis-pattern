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
            var rents = rentQurary.Include(r => r.Book).Where(r => r.BookId == bookId).ToList();

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

        public async Task RentBook(RentBookDTO newRent)
        {
            _ = await _repository._bookRepository.FindAsync(newRent.BookId)
                ?? throw new NotFoundException(string.Format("There is not book with id: {0}", newRent.BookId));
            var rent = _mapper.Map<Rent>(newRent);

            rent.RentDate = DateTime.Now;

            await _repository._rentRepository.Insert(rent);
            await _repository.SaveChanges();
        }

        public async Task ReturnBook(int bookId, string username)
        {
            var rentQurary = await _repository._rentRepository.GetAllAsync();
            var rent = rentQurary.Where(r => r.Username.Equals(username) && r.BookId == bookId).FirstOrDefault()
                ?? throw new NotFoundException(string.Format("User {0} didn't order book with id {1}", username, bookId));

            rent.ReturnDate = DateTime.Now;
            rent.IsReturned = true;
        }
    }
}

using BookService.Interface;
using BookService.Model;
using Microsoft.EntityFrameworkCore;

namespace BookService.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly DbContext _context;

        public IGenericRepository<Book> _bookRepository { get; } = null!;

        public IGenericRepository<Author> _authorRepository { get; } = null!;

        public IGenericRepository<Rent> _rentRepository { get; } = null!;


        public RepositoryWrapper(DbContext context, IGenericRepository<Book> bookRepository, IGenericRepository<Author> authorRepository,
            IGenericRepository<Rent> rentRepository)
        {
            _context = context;
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _rentRepository = rentRepository;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }

        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}

using BookService.Model;

namespace BookService.Interface
{
    public interface IRepositoryWrapper : IDisposable
    {
        IGenericRepository<Book> _bookRepository { get; }
        IGenericRepository<Author> _authorRepository { get; }
        IGenericRepository<Rent> _rentRepository { get; }

        Task SaveChanges();
    }
}

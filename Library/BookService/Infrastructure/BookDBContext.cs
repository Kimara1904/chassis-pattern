using BookService.Model;
using Microsoft.EntityFrameworkCore;

namespace BookService.Infrastructure
{
    public class BookDBContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Rent> Rents { get; set; }

        public BookDBContext(DbContextOptions<BookDBContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookDBContext).Assembly);
        }
    }
}

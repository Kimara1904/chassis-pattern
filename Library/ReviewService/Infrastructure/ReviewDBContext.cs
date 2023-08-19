using Microsoft.EntityFrameworkCore;
using ReviewService.Model;

namespace ReviewService.Infrastructure
{
    public class ReviewDBContext : DbContext
    {
        public DbSet<Review> Reviews { get; set; }

        public ReviewDBContext(DbContextOptions<ReviewDBContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReviewDBContext).Assembly);
        }
    }
}

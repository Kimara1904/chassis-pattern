using BookService.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookService.Infrastructure.Configurations
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasQueryFilter(x => !x.Deleted);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(30);
            builder.Property(x => x.Description).HasMaxLength(70);
            builder.Property(x => x.Deleted).HasDefaultValue(false);
            builder.HasOne(x => x.Author).WithMany(a => a.Books).HasForeignKey(x => x.AuthorId);
        }
    }
}

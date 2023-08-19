using BookService.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookService.Infrastructure.Configurations
{
    public class RentConfiguration : IEntityTypeConfiguration<Rent>
    {
        public void Configure(EntityTypeBuilder<Rent> builder)
        {
            builder.HasQueryFilter(x => !x.IsDeleted);
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Username).IsRequired();
            builder.Property(x => x.RentDate).IsRequired();
            builder.Property(x => x.ReturnDate).HasDefaultValue(null);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.Property(x => x.IsReturned).HasDefaultValue(false);
            builder.HasOne(x => x.Book).WithMany(b => b.Rents).HasForeignKey(x => x.BookId);
        }
    }
}

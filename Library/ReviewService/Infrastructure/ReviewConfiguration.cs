using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ReviewService.Model;

namespace ReviewService.Infrastructure
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.BookId).IsRequired();
            builder.Property(x => x.Comment).IsRequired();
            builder.Property(x => x.IsVerified).HasDefaultValue(false);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
        }
    }
}

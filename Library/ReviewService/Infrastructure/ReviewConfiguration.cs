using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReviewService.Enums;
using ReviewService.Model;

namespace ReviewService.Infrastructure
{
    public class ReviewConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.BookId).IsRequired();
            builder.Property(x => x.Username).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.Comment).IsRequired();
            builder.Property(x => x.Verified).HasConversion(new EnumToStringConverter<ReviewVerifiedState>()).HasDefaultValue(ReviewVerifiedState.Waiting);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
        }
    }
}

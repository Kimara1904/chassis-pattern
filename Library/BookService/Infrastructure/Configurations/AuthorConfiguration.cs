﻿using BookService.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookService.Infrastructure.Configurations
{
    public class AuthorConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasQueryFilter(a => !a.IsDeleted);
            builder.HasKey(a => a.Id);
            builder.Property(a => a.FirstName).IsRequired().HasMaxLength(20);
            builder.Property(a => a.LastName).IsRequired().HasMaxLength(20);
            builder.Property(a => a.IsDeleted).HasDefaultValue(false);
        }
    }
}

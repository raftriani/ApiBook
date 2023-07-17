using ApiBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiBook.Infra.Data.Mappings
{
    internal class BookMapping : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Title)
                .IsRequired()
                .HasField("varchar(100)");

            builder.Property(u => u.Author)
                .IsRequired()
                .HasField("varchar(100)");

            builder.Property(u => u.LaunchYear)
                .IsRequired()
                .HasField("int(4)");

            builder.Property(u => u.BookCover)
                .IsRequired()
                .HasField("varchar(max)");
        }
    }
}

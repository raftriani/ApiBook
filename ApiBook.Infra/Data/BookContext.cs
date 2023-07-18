using ApiBook.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace ApiBook.Infra.Data
{
    [ExcludeFromCodeCoverage]
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options) { }

        public DbSet<Book> Book { get; set; }
    }
}

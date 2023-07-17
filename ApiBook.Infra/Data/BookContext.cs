using ApiBook.Domain.Entities;
using ApiBook.Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace ApiBook.Infra.Data
{
    public class BookContext : DbContext
    {
        public BookContext(DbContextOptions<BookContext> options) : base(options) { }

        public DbSet<Book> Book { get; set; }
    }
}

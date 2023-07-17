using ApiBook.Domain.Entities;
using ApiBook.Domain.Repositories;
using ApiBook.Domain.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ApiBook.Infra.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookContext _context;

        public BookRepository(BookContext cashierContext)
        {
            _context = cashierContext;
        }

        public async Task<Book> GetBook()
        {
            // Como existe apenas uma conta no exemplo, será feito dessa forma
            return await _context.Book.FirstAsync();
        }

        public virtual async Task<IEnumerable<Book>> FindAll()
        {
            return await _context.Book.ToListAsync();
        }

        public virtual async Task<Book> GetById(Guid id)
        {
            return await _context.Book.FindAsync(id);
        }
        public virtual async Task Add(Book book)
        {
            _context.Book.Add(book);
            await SaveChanges();
        }

        public virtual async Task Update(Book book)
        {
            _context.Book.Update(book);
            await SaveChanges();
        }

        public virtual async Task Remove(Guid id)
        {
            _context.Book.Remove(new Book { Id = id });
            await SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }

        public async Task<int> SaveChanges()
        {
            var result = await _context.SaveChangesAsync();

            return result;
        }
    }
}

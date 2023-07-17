using ApiBook.Domain.Entities;
using ApiBook.Domain.Repositories.Base;

namespace ApiBook.Domain.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<IEnumerable<Book>> FindAll();
        Task<Book> GetById(Guid id);
        Task Add(Book book);
        Task Update(Book book);
        Task Remove(Guid id);
    }
}

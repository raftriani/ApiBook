using ApiBook.Domain.Entities;

namespace ApiBook.Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> FindAll();
        Task<Book> GetById(Guid id);
        Task<string> Add(Book book);
        Task<string> Update(Book book);
        Task Delete(Guid id);
    }
}

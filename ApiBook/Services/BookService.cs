using ApiBook.Domain.Entities;
using ApiBook.Domain.Repositories;
using ApiBook.Infra.Data.Repositories;

namespace ApiBook.Services
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<Book>> FindAll()
        {
            return await _bookRepository.FindAll();
        }

        public async Task<Book> GetById(Guid id) 
        {
            return await _bookRepository.GetById(id);
        }

        public async Task Add(Book book)
        {
            await _bookRepository.Add(book);
        }

        public async Task Update(Book book)
        {
            await _bookRepository.Update(book);
        }

        public async Task Delete(Guid id) 
        {
            await _bookRepository.Remove(id);
        }
    }
}

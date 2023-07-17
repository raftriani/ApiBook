using ApiBook.Domain.Entities;
using ApiBook.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiLivros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ObjectResult> FindAll()
        {
            try
            {
                var result = await _bookService.FindAll();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ObjectResult> Find(Guid id)
        {
            try
            {
                var result = await _bookService.GetById(id);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPost]
        public async Task<ObjectResult> Add(Book book)
        {
            try
            {
                await _bookService.Add(book);

                return Ok("Livro inserido com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPatch]
        public async Task<ObjectResult> Update(Book book)
        {
            try
            {
                await _bookService.Update(book);

                return Ok("Livro atualizado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ObjectResult> Remove(Guid id)
        {
            try
            {
                await _bookService.Delete(id);

                return Ok("Livro removido com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }
    }
}

using ApiBook.Application.Interfaces;
using ApiBook.Domain.Entities;
using ApiBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiLivros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
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
                string message = await _bookService.Add(book);

                if(string.IsNullOrEmpty(message))
                {
                    return Ok("Livro inserido com sucesso!");
                }
                else
                {
                    return Problem(message);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPatch]
        public async Task<ObjectResult> Update(BookViewModel book)
        {
            try
            {
                Book bookModel = BookViewModel.Factory(book);

                string message = await _bookService.Update(bookModel);

                if (string.IsNullOrEmpty(message))
                {
                    return Ok("Livro atualizado com sucesso!");
                }
                else
                {
                    return Problem(message);
                }
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

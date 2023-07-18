using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ApiBook.Application.Interfaces;
using ApiBook.Domain.Entities;
using ApiBook.Models;
using ApiLivros.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ApiBook.UnitTests.Controllers
{
    public class BookControllerTest
    {
        private readonly Mock<IBookService> _bookServiceMock = new Mock<IBookService>();
        private readonly BookController _bookController;

        public BookControllerTest()
        {
            _bookController = new BookController(_bookServiceMock.Object);
        }

        [Fact]
        public async Task FindAll_ShouldReturnOkResult_WhenCalled()
        {
            // Arrange
            var fakeBooks = new List<Book>(){
                new Book(),
                new Book()
            };
            _bookServiceMock.Setup(x => x.FindAll()).ReturnsAsync(fakeBooks);

            // Act
            var result = await _bookController.FindAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var books = Assert.IsAssignableFrom<IEnumerable<Book>>(okResult.Value);
            Assert.Equal(fakeBooks.Count, books.Count());
        }

        [Fact]
        public async Task Find_ShouldReturnOkResult_WhenCalled()
        {
            // Arrange
            var fakeId = Guid.NewGuid();
            var fakeBook = new Book();
            _bookServiceMock.Setup(x => x.GetById(fakeId)).ReturnsAsync(fakeBook);

            // Act
            var result = await _bookController.Find(fakeId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var book = Assert.IsType<Book>(okResult.Value);
            Assert.Equal(fakeBook, book);
        }

        [Fact]
        public async Task Add_ShouldReturnOkResult_WhenCalledWithValidData()
        {
            // Arrange
            var fakeBook = new Book(){
                Id = Guid.NewGuid(),
                Title = "Mocked Book",
                Author = "Mocked Author"
            };
            _bookServiceMock.Setup(x => x.Add(fakeBook)).ReturnsAsync(string.Empty);

            // Act
            var result = await _bookController.Add(fakeBook);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Livro inserido com sucesso!", okResult.Value);
        }

        [Fact]
        public async Task Add_ShouldReturnProblemResult_WhenCalledWithInvalidData()
        {
            // Arrange
            var fakeBook = new Book();
            _bookServiceMock.Setup(x => x.Add(fakeBook)).ReturnsAsync("An error occurred");

            // Act
            var result = await _bookController.Add(fakeBook);

            // Assert
            var problemResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, problemResult.StatusCode);
        }

        [Fact]
        public async Task Update_ShouldReturnOkResult_WhenCalledWithValidData()
        {
            // Arrange
            var fakeBookViewModel = new BookViewModel(){
                Id = Guid.NewGuid(),
                Title = "Mocked Book",
                Author = "Mocked Author"
            };
            var fakeBook = new Book(){
                Id = fakeBookViewModel.Id,
                Title = fakeBookViewModel.Title,
                Author = fakeBookViewModel.Author
            };
            _bookServiceMock.Setup(x => x.Update(fakeBook)).ReturnsAsync(string.Empty);

            // Act
            var result = await _bookController.Update(fakeBookViewModel);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Livro atualizado com sucesso!", okResult.Value);
        }

        [Fact]
        public async Task Update_ShouldReturnProblemResult_WhenCalledWithInvalidData()
        {
            // Arrange
            var fakeBookViewModel = new BookViewModel();
            _bookServiceMock.Setup(x => x.Update(It.IsAny<Book>())).ReturnsAsync("An error occurred");

            // Act
            var result = await _bookController.Update(fakeBookViewModel);

            // Assert
            var problemResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, problemResult.StatusCode);
        }

        [Fact]
        public async Task Remove_ShouldReturnOkResult_WhenCalledWithExistingId()
        {
            // Arrange
            var fakeId = Guid.NewGuid();

            // Act
            var result = await _bookController.Remove(fakeId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Livro removido com sucesso!", okResult.Value);
            _bookServiceMock.Verify(x => x.Delete(fakeId), Times.Once);
        }

        [Fact]
        public async Task Remove_ShouldReturnBadRequestResult_WhenCalledWithNonexistentId()
        {
            // Arrange
            var fakeId = Guid.NewGuid();
            _bookServiceMock.Setup(x => x.Delete(fakeId)).Throws(new Exception());

            // Act
            var result = await _bookController.Remove(fakeId);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.NotNull(badRequestResult.Value);
        }
    }
}

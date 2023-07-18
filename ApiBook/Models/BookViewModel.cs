using ApiBook.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace ApiBook.Models
{
    public class BookViewModel
    {
        [Required(ErrorMessage = "O campo{0} é obrigatório")]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int LaunchYear { get; set; }
        public string Cover { get; set; }
        public static Book Factory(BookViewModel bookViewModel) 
        {
           Book book = new Book
           {
               Id = bookViewModel.Id,
               Title = bookViewModel.Title,
               Author = bookViewModel.Author,
               LaunchYear = bookViewModel.LaunchYear, 
               Cover = bookViewModel.Cover
           };

            return book;
        }
    }
}

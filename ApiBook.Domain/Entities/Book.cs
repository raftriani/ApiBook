using ApiBook.Domain.Repositories.Base;
using ApiLivros.Domain.Repositories.Base;
using System.ComponentModel.DataAnnotations;

namespace ApiBook.Domain.Entities
{
    public class Book : Entity, IAggregationInterface
    {
        public Book(string title, string author, int launchYear, string cover)
        {
            Title = title;
            Author = author;
            LaunchYear = launchYear;
            Cover = cover;
        }
        [Required(ErrorMessage ="O campo{0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo{0} precisa ter entre {2} e {1} caractectes", MinimumLength =10)]
        public string Title { get; set; }
        [Required(ErrorMessage = "O campo{0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo{0} precisa ter entre {2} e {1} caractectes", MinimumLength = 10)]
        public string Author { get; set; }
        [Required(ErrorMessage = "O campo{0} é obrigatório")]
        public int LaunchYear { get; set; }
        [Required(ErrorMessage = "O campo{0} é obrigatório")]
        public string Cover { get; set; }
        public Book() {}
    }
}

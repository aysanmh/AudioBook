using System.ComponentModel.DataAnnotations;

namespace AudioBookApi.Dtos.Book
{
    public class CreateBookRequestDto
    {
        [Required]
        [MaxLength(150,ErrorMessage = "Title cannot be more than 150 characters")]
   
        public string Title { get; set; } = string.Empty;

        [Required]
        [MaxLength(50, ErrorMessage = "Author cannot be more than 50 characters")]
        public string Author { get; set; } = string.Empty;

        [Required]
        [MaxLength(50, ErrorMessage = "Narrator cannot be more than 50 characters")]
        public string Narrator { get; set; } = string.Empty;

        [Required]
        [MaxLength(20, ErrorMessage = "Genre cannot be more than 20 characters")]
        public string Genre { get; set; } = string.Empty;
    }
}

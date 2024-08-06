using AudioBookApi.Dtos.Book;
using AudioBookApi.Models;

namespace AudioBookApi.Mappers
{
    public static class BookMappers
    {
        public static BookDto ToBookDto(this Book bookModel)
        {
            return new BookDto
            {
                Id = bookModel.Id,
                Title = bookModel.Title,
                Author = bookModel.Author,
                Narrator = bookModel.Narrator,
                Genre = bookModel.Genre,
                
            };

        }
        public static Book ToBookFromCreateDto(this CreateBookRequestDto bookDto)
        {
            return new Book
            {
                Title = bookDto.Title,
                Author = bookDto.Author,
                Narrator = bookDto.Narrator,
                Genre = bookDto.Genre,
            };
        }
        

    }
}

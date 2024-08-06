using AudioBookApi.Dtos.Book;
using AudioBookApi.Helpers;
using AudioBookApi.Models;

namespace AudioBookApi.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetAllAsync(QueryObject query);

        Task<Book?> GetByIdAsync(int id);
        Task<Book?> GetByTitleAsync(string title);

        Task<Book> CreateAsync(Book bookModel);

        Task<Book?> UpdateAsync(int id, UpdateBookRequestDto bookDto);

        Task<Book?> DeleteAsync(int id);

    }
}

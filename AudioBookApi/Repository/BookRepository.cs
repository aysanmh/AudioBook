using AudioBookApi.Data;
using AudioBookApi.Dtos.Book;
using AudioBookApi.Helpers;
using AudioBookApi.Interfaces;
using AudioBookApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AudioBookApi.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Book> CreateAsync(Book bookModel)
        {
            await _context.Books.AddAsync(bookModel);
            await _context.SaveChangesAsync();
            return bookModel;
            
        }

        public async Task<Book?> DeleteAsync(int id)
        {
            var bookModel = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if (bookModel == null)
            {
                return null;
            }
            _context.Books.Remove(bookModel);
            await _context.SaveChangesAsync();
            return bookModel;
        }

        public async Task<List<Book>> GetAllAsync(QueryObject query)
        {
            var books =  _context.Books.AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                books = books.Where(b => b.Title.Contains(query.Title));
            }
            if (!string.IsNullOrWhiteSpace(query.Author))
            {
                books = books.Where(b => b.Author.Contains(query.Author));
            }
            if (!string.IsNullOrWhiteSpace(query.Narrator))
            {
                books = books.Where(b => b.Narrator.Contains(query.Narrator));
            }
            if (!string.IsNullOrWhiteSpace(query.Genre))
            {
                books = books.Where(b => b.Genre.Contains(query.Genre));
            }
            if(!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Author", StringComparison.OrdinalIgnoreCase))
                {
                    books = query.IsDecsending ? books.OrderByDescending(b => b.Author) : books.OrderBy(b => b.Author);

                }
            }

            var skipNumber = (query.PageNumber -1 ) * query.PageSize;

            return  await books.Skip(skipNumber).Take(query.PageSize).ToListAsync();

        }

        public async Task<Book?> GetByIdAsync(int id)
        {
            return await _context.Books.FindAsync(id);
            
        }

        public async Task<Book?> GetByTitleAsync(string title)
        {
            return await _context.Books.FirstOrDefaultAsync(b => b.Title == title);
        }

        public async  Task<Book?> UpdateAsync(int id, UpdateBookRequestDto bookDto)
        {
            var existingBook = await _context.Books.FirstOrDefaultAsync(b => b.Id == id);
            if(existingBook == null)
            {
                return null;
            }
            existingBook.Title = bookDto.Title;
            existingBook.Author = bookDto.Author;
            existingBook.Narrator = bookDto.Narrator;

            await _context.SaveChangesAsync();

            return existingBook;
        }
    }
}

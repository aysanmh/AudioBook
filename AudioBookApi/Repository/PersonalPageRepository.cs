using AudioBookApi.Data;
using AudioBookApi.Interfaces;
using AudioBookApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AudioBookApi.Repository
{
    public class PersonalPageRepository : IPersonalPageRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonalPageRepository(ApplicationDbContext context ) 
        {
            _context = context;
        }

        public async Task<PersonalPage> CreateAsync(PersonalPage page)
        {
            await _context.PersonalPages.AddAsync(page);
            await _context.SaveChangesAsync();
            return page;
        }

        public async Task<List<Book>> GetUserInfo(User user)
        {
            return await _context.PersonalPages.Where(u => u.UserId == user.Id).
                Select(book => new Book
                {
                    Id = book.BookId,
                    Title = book.Book.Title,
                    Author = book.Book.Author,
                    Narrator = book.Book.Narrator,
                    Genre = book.Book.Genre,
                 }).ToListAsync();
        }

        async Task<PersonalPage> IPersonalPageRepository.DeleteProfileAsync(User user, string title)
        {
            var profileModel = await _context.PersonalPages.FirstOrDefaultAsync(u => u.UserId == user.Id && u.Book.Title.ToLower() == title.ToLower());

            if (profileModel == null)
            {
                return null;
            }
            _context.PersonalPages.Remove(profileModel);
            await _context.SaveChangesAsync();
            return profileModel;
        }
    }
}

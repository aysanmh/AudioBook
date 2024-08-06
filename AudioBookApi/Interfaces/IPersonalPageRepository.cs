using AudioBookApi.Models;

namespace AudioBookApi.Interfaces
{
    public interface IPersonalPageRepository
    {
        Task<List<Book>> GetUserInfo(User user);
        Task<PersonalPage> CreateAsync(PersonalPage page);
        Task<PersonalPage> DeleteProfileAsync(User user,string title);
    }
}

using AudioBookApi.Models;

namespace AudioBookApi.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(User user);

    }
}

using WordGame.Models;

namespace WordGame.Services.Contracts
{
    public interface IUserService
    {
        User Register(string userName, string password, string fullName, string email);

        string Login(string userName, string password);

        bool Logout(string authKey);
    }
}

using WordGame.Models;

namespace WordGame.Factories
{
    public interface IUserFactory
    {
        User Register(string userName, string passwordHash, string fullName, string email);
    }
}

using System.Collections.Generic;
using WordGame.Models;

namespace WordGame.Services.Utilities.Contracts
{
    public interface IUserUtility
    {
        IEnumerable<User> GetAll();

        User GetById(string id);
    }
}

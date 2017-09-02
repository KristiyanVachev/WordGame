using WordGame.Models;

namespace WordGame.Services.Contracts
{
    public interface IThreadService
    {
        Thread Create(string authKey, string name, string firstWord);

        Post AddWord(string authKey, int threadId, string word);
    }
}

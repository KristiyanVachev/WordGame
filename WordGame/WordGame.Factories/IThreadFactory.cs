using WordGame.Models;

namespace WordGame.Factories
{
    public interface IThreadFactory
    {
        Thread CreateThread(int userId, string name);

        Post CreatePost(int userId, int threadId, string word);
    }
}

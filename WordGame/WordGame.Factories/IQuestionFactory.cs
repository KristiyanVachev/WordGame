using WordGame.Models;

namespace WordGame.Factories
{
    public interface IQuestionFactory
    {
        Question CreateQuestion(string condition);

        Answer CreateAnswer(string content, bool isCorrect);
    }
}

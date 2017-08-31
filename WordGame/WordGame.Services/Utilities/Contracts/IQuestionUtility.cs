using System.Collections.Generic;
using WordGame.Models;

namespace WordGame.Services.Utilities.Contracts
{
    public interface IQuestionUtility
    {
        IEnumerable<Question> GetQuestions();

        Question CreateQuestion(Submission submission);

        Question GetById(int id);
    }
}

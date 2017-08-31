using System;
using System.Collections.Generic;
using WordGame.Models;
using WordGame.Models.Enums;

namespace WordGame.Factories
{
    public interface ITestFactory
    {
        Test CreateTest(string userId, IEnumerable<Question> questions, TestType type);

        AnsweredQuestion CreateAnsweredQuestion(int testId, int questionId, int answerId);
    }
}

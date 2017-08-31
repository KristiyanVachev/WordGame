using System.Collections.Generic;
using WordGame.Models;
using WordGame.Models.Enums;

namespace WordGame.Services.Utilities.Contracts
{
    public interface ITestUtility
    {
        Test CreateTest(string userId, TestType type, IEnumerable<Question> questions);

        Test GetLastTest(string userId, TestType type);

        Test GetTestById(int testId);

        void AddAnswer(int testId, int questionId, int answerId);

        void RemoveQuestionById(int testId, int questionId);

        bool TestIsFinished(int testId);

        void EndTest(int testId);

        IDictionary<int, int[]> GatherTestStatistics(int testId);
    }
}

using System;
using WordGame.Models;

namespace WordGame.Factories
{
    public interface ISubmitFactory
    {
        Submission CreateSubmission(string userId, string category, string condition, string correctAnswer, DateTime sentOn);

        SubmissionAnswer CreateSubmissionAnswer(string content);
    }
}

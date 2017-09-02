using System.Collections.Generic;
using WordGame.Models;

namespace WordGame.Services.Contracts
{
    public interface IReportService
    {
        Report Create(string authKey, int postId);

        IEnumerable<Report> Reports(string authKey);

        void Process(string authKey, int reportId);
    }
}

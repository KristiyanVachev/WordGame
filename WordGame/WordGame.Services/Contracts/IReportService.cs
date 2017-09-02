using WordGame.Models;

namespace WordGame.Services.Contracts
{
    public interface IReportService
    {
        Report Create(string authKey, int postId);
    }
}

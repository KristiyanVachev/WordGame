using WordGame.Models;

namespace WordGame.Factories
{
    public interface IReportFactory
    {
        Report CreateReport(int userId, int postId);
    }
}

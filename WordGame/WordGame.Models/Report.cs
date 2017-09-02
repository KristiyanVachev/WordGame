using System.ComponentModel.DataAnnotations.Schema;

namespace WordGame.Models
{
    public class Report
    {
        public Report(int userId, int postId)
        {
            this.UserId = userId;
            this.PostId = postId;
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public int PostId { get; set; }
    }
}

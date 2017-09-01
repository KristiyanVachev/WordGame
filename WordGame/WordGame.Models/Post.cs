using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordGame.Models
{
    public class Post
    {
        public Post()
        {

        }

        public Post(int userId, int threadId, string word) : this()
        {
            this.UserId = userId;
            this.ThreadId = threadId;
            this.Word = word;
        }

        public int Id { get; set; }

        public string Word { get; set; }

        //Author
        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public int? ThreadId { get; set; }

        [ForeignKey("ThreadId")]
        public virtual Thread Thread { get; set; }
    }
}

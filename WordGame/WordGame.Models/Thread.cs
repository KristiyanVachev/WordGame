using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WordGame.Models
{
    public class Thread
    {
        private ICollection<Post> posts;

        public Thread()
        {
            this.posts = new HashSet<Post>();
        }

        public Thread(int userId, string name) : this()
        {
            this.UserId = userId;
            this.Name = name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int WordCount { get; set; }

        //Author
        public int? UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual ICollection<Post> Posts
        {
            get { return this.posts; }
            set { this.posts = value; }
        }
    }
}

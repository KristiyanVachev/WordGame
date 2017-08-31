using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WordGame.Models
{
    public class User : IdentityUser
    {
        private ICollection<Thread> threads;
        private ICollection<Post> posts;

        public User()
        {
            this.threads = new HashSet<Thread>();
            this.posts = new HashSet<Post>();
        }

        public User(string username) : this()
        {
            this.UserName = username;
        }
        
        public virtual ICollection<Thread> Threads
        {
            get { return this.threads; }
            set { this.threads = value; }
        }

        public virtual ICollection<Post> Posts
        {
            get { return this.posts; }
            set { this.posts = value; }
        }

    }
}

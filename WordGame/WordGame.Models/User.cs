using System.Collections.Generic;

namespace WordGame.Models
{
    public class User
    {
        private ICollection<Thread> threads;
        private ICollection<Post> posts;

        public User()
        {
            this.threads = new HashSet<Thread>();
            this.posts = new HashSet<Post>();
        }

        public User(string userName, string passwordHash, string fullName, string email) : this()
        {
            this.UserName = userName;
            this.PasswordHash = passwordHash;
            this.FullName = fullName;
            this.Email = email;
        }

        public int Id { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public bool IsAdmin { get; set; }

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

namespace WordGame.Models
{
    public class RegisteredUser
    {
        public RegisteredUser(int id, string username, string passwordHash, string fullname, string email, bool isAdmin)
        {
            this.Id = id;
            this.UserName = username;
            this.PasswordHash = passwordHash;
            this.FullName = fullname;
            this.Email = email;
            this.IsAdmin = isAdmin;
        }

        public int Id { get; set; }

        public string UserName { get; set; }

        public string PasswordHash { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public bool IsAdmin { get; set; }
    }
}
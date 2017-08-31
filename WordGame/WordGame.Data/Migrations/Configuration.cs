using System.Data.Entity.Migrations;
using System.Linq;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using WordGame.Models;

namespace WordGame.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<WordGameDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "Leaf.Data.LeafDbContext";
        }

        protected override void Seed(WordGameDbContext context)
        {
            if (context.Users.Any())
            {
                return;
            }
           
            if (!(context.Users.Any(u => u.UserName == "typhon")))
            {
                var userStore = new UserStore<User>(context);
                var userManager = new UserManager<User>(userStore);
                var userToInsert = new User { UserName = "typhon", Email = "typhon04@gmail.com" };
                userManager.Create(userToInsert, "nanana");
            }

            context.SaveChanges();
        }
    }
}

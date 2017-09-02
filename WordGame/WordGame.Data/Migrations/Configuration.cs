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
            
        }
    }
}

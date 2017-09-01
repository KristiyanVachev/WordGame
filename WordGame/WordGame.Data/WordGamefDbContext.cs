using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using WordGame.Data.Contracts;
using WordGame.Data.Migrations;
using WordGame.Models;

namespace WordGame.Data
{
    public class WordGameDbContext : DbContext, IWordGameDbContext
    {
        public WordGameDbContext() : this("WordGameDb")
        {
        }

        public WordGameDbContext(string nameOfConnectionString)
            : base(nameOfConnectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<WordGameDbContext, Configuration>());
            //Database.SetInitializer(new DropCreateDatabaseAlways<LeafDbContext>());
        }

        public virtual IDbSet<Thread> Threads { get; set; }

        public virtual IDbSet<Post> Posts { get; set; }

        public virtual IDbSet<User> Users { get; set; }

        public static WordGameDbContext Create()
        {
            return new WordGameDbContext();
        }

        public IDbSet<TEntity> DbSet<TEntity>() where TEntity : class
        {
            return this.Set<TEntity>();
        }

        public void SetAdded<TEntry>(TEntry entity) where TEntry : class
        {
            var entry = this.Entry(entity);
            entry.State = EntityState.Added;
        }

        public void SetDeleted<TEntry>(TEntry entity) where TEntry : class
        {
            var entry = this.Entry(entity);
            entry.State = EntityState.Deleted;
        }

        public void SetUpdated<TEntry>(TEntry entity) where TEntry : class
        {
            var entry = this.Entry(entity);
            entry.State = EntityState.Modified;
        }
    }
}

using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using WordGame.Data.Contracts;
using WordGame.Data.Migrations;
using WordGame.Models;

namespace WordGame.Data
{
    public class WordGameDbContext : IdentityDbContext<IdentityUser>, IWordGameDbContext
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

        public virtual IDbSet<Test> Tests { get; set; }

        public virtual IDbSet<Topic> Topics { get; set; }

        public virtual IDbSet<Category> Categories { get; set; }

        public virtual IDbSet<Question> Questions { get; set; }

        public virtual IDbSet<Answer> Answers { get; set; }

        public virtual IDbSet<AnsweredQuestion> AnsweredQuestions { get; set; }

        public virtual IDbSet<Submission> Submissions { get; set; }

        public virtual IDbSet<CategoryStatistic> CategoryStatistics { get; set; }

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

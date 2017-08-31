using System.Linq;
using Bytes2you.Validation;
using WordGame.Data.Contracts;

namespace WordGame.Data
{
    public class Repository<T> : IRepository<T>
      where T : class
    {
        private readonly IWordGameDbContext dbContext;

        public Repository(IWordGameDbContext dbContext)
        {
            Guard.WhenArgument(dbContext, "dbContext cannot be null").IsNull().Throw();

            this.dbContext = dbContext;
        }

        public IQueryable<T> Entities => this.dbContext.DbSet<T>();

        public void Add(T entity)
        {
            this.dbContext.SetAdded(entity);
        }

        public void Delete(T entity)
        {
            this.dbContext.SetDeleted(entity);
        }

        public T GetById(object id)
        {
            return this.dbContext.DbSet<T>().Find(id);
        }

        public void Update(T entity)
        {
            this.dbContext.SetUpdated(entity);
        }
    }
}

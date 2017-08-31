using System;
using WordGame.Data.Contracts;

namespace WordGame.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IWordGameDbContext dbContext;

        public UnitOfWork(IWordGameDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException("dbContext cannot be null");
            }

            this.dbContext = dbContext;
        }

        public void Dispose()
        {

        }

        public void Commit()
        {
            this.dbContext.SaveChanges();
        }
    }
}

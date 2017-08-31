using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WordGame.Data.Contracts
{
    public interface IWordGameDbContext
    {
        IDbSet<TEntity> DbSet<TEntity>()
            where TEntity : class;

        int SaveChanges();
        
        void SetAdded<TEntry>(TEntry entity)
           where TEntry : class;

        void SetDeleted<TEntry>(TEntry entity)
            where TEntry : class;

        void SetUpdated<TEntry>(TEntry entity)
            where TEntry : class;
    }
}

using Ninject.Modules;
using Ninject.Web.Common;
using WordGame.Data;
using WordGame.Data.Contracts;

namespace Leaf.Web.App_Start.NinjectModules
{
    public class DataNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IWordGameDbContext>().To<WordGameDbContext>().InRequestScope();
            this.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            this.Bind(typeof(IRepository<>)).To(typeof(Repository<>)).InRequestScope();
        }
    }
}
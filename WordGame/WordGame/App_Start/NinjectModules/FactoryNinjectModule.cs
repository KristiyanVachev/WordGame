using Ninject.Modules;
using Ninject.Extensions.Factory;
using WordGame.Factories;

namespace Leaf.Web.App_Start.NinjectModules
{
    public class FactoryNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IUserFactory>().ToFactory().InSingletonScope();
            this.Bind<IThreadFactory>().ToFactory().InSingletonScope();
        }
    }
}
using Ninject.Modules;
using Ninject.Extensions.Factory;
using WordGame.Factories;

namespace Leaf.Web.App_Start.NinjectModules
{
    public class FactoryNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<ITestFactory>().ToFactory().InSingletonScope();
            this.Bind<IQuestionFactory>().ToFactory().InSingletonScope();
            this.Bind<IUserFactory>().ToFactory().InSingletonScope();
        }
    }
}
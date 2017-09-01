using Ninject.Modules;
using WordGame.Services;
using WordGame.Services.Contracts;

namespace Leaf.Web.App_Start.NinjectModules
{
    public class ServiceNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IUserService>().To<UserService>();
            this.Bind<IThreadService>().To<ThreadService>();
        }
    }
}
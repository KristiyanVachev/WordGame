using Ninject.Modules;
using WordGame.Services.Utilities;
using WordGame.Services.Utilities.Contracts;

namespace Leaf.Web.App_Start.NinjectModules
{
    public class ServiceNinjectModule : NinjectModule
    {
        public override void Load()
        {
            //this.Bind<ITestUtility>().To<TestUtility>();
            //this.Bind<IQuestionUtility>().To<QuestionUtility>();
            this.Bind<IUserUtility>().To<UserUtility>();
        }
    }
}
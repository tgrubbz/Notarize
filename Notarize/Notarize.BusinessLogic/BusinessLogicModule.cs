using Ninject.Modules;
using Notarize.BusinessLogic.Interfaces;
using Notarize.BusinessLogic.Managers;
using Notarize.DataAccess;

namespace Notarize.BusinessLogic
{
    public class BusinessLogicModule : NinjectModule
    {
        string ConnectionString;

        /// <summary>
        /// Loads the Business Logic Ninject Module
        /// </summary>
        public BusinessLogicModule(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public override void Load()
        {
            Kernel.Load(new INinjectModule[] { new DataAccessModule(ConnectionString) });

            Bind<IUserManager>().To<UserManager>();
        }
    }
}

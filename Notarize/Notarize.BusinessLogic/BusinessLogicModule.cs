using Ninject.Modules;
using Notarize.DataAccess;

namespace Notarize.BusinessLogic
{
    public class BusinessLogicModule : NinjectModule
    {
        /// <summary>
        /// Loads the Business Logic Ninject Module
        /// </summary>
        public BusinessLogicModule(string connectionString)
        {
            Kernel.Load(new INinjectModule[] { new DataAccessModule(connectionString) });
        }

        public override void Load()
        {
        }
    }
}

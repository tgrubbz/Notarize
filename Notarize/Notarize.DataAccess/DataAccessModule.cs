using Ninject.Modules;
using Notarize.DataAccess.Interfaces;
using Notarize.DataAccess.Repositories;
using System.Data;
using System.Data.SqlClient;

namespace Notarize.DataAccess
{
    public class DataAccessModule : NinjectModule
    {
        /// <summary>
        /// Loads the Business Logic Ninject Module
        /// </summary>
        public DataAccessModule(string connectionString)
        {
            Bind<IDbConnection>().ToMethod(x => new SqlConnection(connectionString));
        }

        public override void Load()
        {
            Bind<IUserRepository>().To<UserRepository>();
        }
    }
}

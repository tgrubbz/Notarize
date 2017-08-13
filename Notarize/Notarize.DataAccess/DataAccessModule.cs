using Ninject.Modules;
using Notarize.DataAccess.Interfaces;
using Notarize.DataAccess.Repositories;
using System.Data;
using System.Data.SqlClient;

namespace Notarize.DataAccess
{
    public class DataAccessModule : NinjectModule
    {
        string ConnectionString;

        /// <summary>
        /// Loads the Business Logic Ninject Module
        /// </summary>
        public DataAccessModule(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public override void Load()
        {
            Bind<IDbConnection>().ToMethod(x => new SqlConnection(ConnectionString));

            Bind<IUserRepository>().To<UserRepository>();
        }
    }
}

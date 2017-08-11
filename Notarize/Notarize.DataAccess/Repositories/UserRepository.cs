using Dapper;
using Notarize.Core.Models;
using Notarize.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Notarize.DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        IDbConnection db;

        public UserRepository(IDbConnection connection)
        {
            db = connection;
        }

        public int Create(User user, string passwordHash)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("Username", user.Username, DbType.String, size: 256);
            p.Add("PasswordHash", passwordHash, DbType.String);
            p.Add("Role", user.Role, DbType.Int32);

            using (SqlConnection conn = new SqlConnection(db.ConnectionString))
            {
                return conn.Query<int>("INSERT INTO Users (Username, PasswordHash, Role) OUTPUT INSERTED.Id VALUES (@Username, @PasswordHash, @Role)", p).SingleOrDefault();
            }
        }

        public int Delete(int id)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("Id", id, DbType.Int32);

            using (SqlConnection conn = new SqlConnection(db.ConnectionString))
            {
                return conn.Execute("DELETE FROM Users WHERE Id = @Id", p);
            }
        }

        public IEnumerable<User> GetAll()
        {
            using (SqlConnection conn = new SqlConnection(db.ConnectionString))
            {
                return conn.Query<User>("SELECT * FROM Users");
            }
        }

        public User Read(int id)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("Id", id, DbType.Int32);

            using (SqlConnection conn = new SqlConnection(db.ConnectionString))
            {
                return conn.Query<User>("SELECT * FROM Users WHERE Id = @Id", p).SingleOrDefault();
            }
        }

        public User Read(string username)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("Username", username, DbType.String, size: 256);

            using (SqlConnection conn = new SqlConnection(db.ConnectionString))
            {
                return conn.Query<User>("SELECT * FROM Users WHERE Username = @Username", p).SingleOrDefault();
            }
        }

        public int Update(User user)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("Id", user.Id, DbType.Int32);
            p.Add("Username", user.Username, DbType.String, size: 256);
            p.Add("Role", user.Role, DbType.Int32);

            using (SqlConnection conn = new SqlConnection(db.ConnectionString))
            {
                return conn.Execute("UPDATE Users SET Username = @Username, Role = @Role WHERE Id = @Id", p);
            }
        }

        public int UpdatePassword(int id, string passwordHash)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("Id", id, DbType.Int32);
            p.Add("PasswordHash", passwordHash, DbType.String);

            using (SqlConnection conn = new SqlConnection(db.ConnectionString))
            {
                return conn.Execute("UPDATE Users SET PasswordHash = @PasswordHash WHERE Id = @Id", p);
            }
        }

        public string GetPasswordHash(int id)
        {
            DynamicParameters p = new DynamicParameters();
            p.Add("Id", id, DbType.Int32);

            using (SqlConnection conn = new SqlConnection(db.ConnectionString))
            {
                return conn.Query<string>("SELECT PasswordHash FROM Users WHERE Id = @Id", p).SingleOrDefault();
            }
        }
    }
}

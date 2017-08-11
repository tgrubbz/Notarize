using Notarize.Core.Models;
using System.Collections.Generic;

namespace Notarize.DataAccess.Interfaces
{
    public interface IUserRepository
    {
        int Create(User user, string paswordHash);
        User Read(string username);
        User Read(int id);
        int Update(User user);
        int Delete(int id);
        IEnumerable<User> GetAll();
        int UpdatePassword(int id, string passwordHash);
        string GetPasswordHash(int id);
    }
}

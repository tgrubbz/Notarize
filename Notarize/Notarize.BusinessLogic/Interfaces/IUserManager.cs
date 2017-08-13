using Notarize.BusinessLogic.Models;
using Notarize.Core.Models;

namespace Notarize.BusinessLogic.Interfaces
{
    public interface IUserManager
    {
        int Create(User user, string password);
        User Read(string username);
        int Update(User user);
        int Delete(int id);
        LoginResult Login(string username, string password);
        int UpdatePassword(string username, string oldPassword, string newPassword);
    }
}

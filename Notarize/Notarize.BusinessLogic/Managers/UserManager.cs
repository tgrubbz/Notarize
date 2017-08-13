using Notarize.BusinessLogic.Interfaces;
using Notarize.BusinessLogic.Models;
using Notarize.Core.Enumerations;
using Notarize.Core.Models;
using Notarize.DataAccess.Interfaces;
using System.Collections.Generic;
using System.Security.Claims;

namespace Notarize.BusinessLogic.Managers
{
    public class UserManager : IUserManager
    {
        IUserRepository UserRepo;

        public UserManager(IUserRepository userRepo)
        {
            UserRepo = userRepo;
        }

        public int Create(User user, string password)
        {
            return UserRepo.Create(user, PasswordHasher.HashPassword(password));
        }

        public User Read(string username)
        {
            return UserRepo.Read(username);
        }

        public int Update(User user)
        {
            return UserRepo.Update(user);
        }

        public int Delete(int id)
        {
            return UserRepo.Delete(id);
        }

        public LoginResult Login(string username, string password)
        {
            LoginResult result = new LoginResult();
            User user = UserRepo.Read(username);

            // Check that user exists
            if (user != null)
            {
                // Get the password hash
                string passwordHash = UserRepo.GetPasswordHash(user.Id);

                // Verify the entered password
                if (PasswordHasher.VerifyHashedPassword(passwordHash, password))
                {
                    // Create the identity
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim("Username", user.Username));
                    claims.Add(new Claim("Role", user.Role.ToClaimString()));
                    claims.Add(new Claim(ClaimTypes.Name, user.Username));
                    
                    result.Identity = new ClaimsIdentity(claims, "AuthenticationCookie");

                    result.Success = true;
                }
                else
                {
                    result.Error = "Incorrect password";
                }
            }
            else
            {
                result.Error = "Username does not exist";
            }

            return result;
        }

        public int UpdatePassword(string username, string oldPassword, string newPassword)
        {
            // Check that the new password is different
            if (string.Equals(oldPassword, newPassword))
            {
                return 0;
            }

            User user = UserRepo.Read(username);

            // Check that the user exists
            if (user != null)
            {
                string passwordHash = UserRepo.GetPasswordHash(user.Id);

                // Verify that the old password is correct
                if (PasswordHasher.VerifyHashedPassword(passwordHash, oldPassword))
                {
                    return UserRepo.UpdatePassword(user.Id, PasswordHasher.HashPassword(newPassword));
                }
            }

            return 0;
        }
    }
}

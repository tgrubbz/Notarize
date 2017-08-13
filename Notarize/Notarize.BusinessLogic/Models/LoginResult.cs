using Notarize.Core.Models;
using System.Security.Claims;

namespace Notarize.BusinessLogic.Models
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public ClaimsIdentity Identity { get; set; }
        public User User { get; set; }
    }
}

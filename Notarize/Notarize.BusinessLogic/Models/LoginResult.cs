using System.Security.Claims;

namespace Notarize.BusinessLogic.Models
{
    public class LoginResult
    {
        public bool Success { get; set; }
        public string Error { get; set; }
        public ClaimsPrincipal Principal { get; set; }
    }
}

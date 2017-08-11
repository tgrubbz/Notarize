using Notarize.Core.Enumerations;

namespace Notarize.Core.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public UserRole Role { get; set; }
        // TODO: Implement if neccessary
        //public List<Claim> Claims { get; set; }
    }
}

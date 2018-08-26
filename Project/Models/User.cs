using Microsoft.AspNetCore.Identity;

namespace Project.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
    }
}

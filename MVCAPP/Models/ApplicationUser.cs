using Microsoft.AspNetCore.Identity;

namespace MVCAPP.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string City { get; set; }
    }
}

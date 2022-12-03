using MVCAPP.Models;
using System.Collections.Generic;
using System.Security.Claims;

namespace MVCAPP.ViewModels
{
    public class UserClaimViewModel
    {
        public UserClaimViewModel()
        {
            Claims = new List<UserClaim>();
        }
        public string UserId { get; set; }
        public List<UserClaim> Claims { get; set; }


    }
}

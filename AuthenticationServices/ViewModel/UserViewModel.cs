using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationServices.ViewModel
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Mobile_Number { get; set; }
        public RoleViewModel[] Roles { get; set; }
    }
}

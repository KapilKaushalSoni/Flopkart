using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationServices.Models
{
    public class AppUser:IdentityUser
    {
        public string Mobile_Number { get; set; }
        public bool Is_Active { get; set; }
    }
}

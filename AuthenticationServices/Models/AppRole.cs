using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationServices.Models
{
    public class AppRole:IdentityRole
    {
        public string Role_Description { get; set; }
        public bool Is_Active { get; set; }
    }
}

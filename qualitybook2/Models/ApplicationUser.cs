using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace qualitybook2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
        public override DateTimeOffset? LockoutEnd { get; set; }
    }
}

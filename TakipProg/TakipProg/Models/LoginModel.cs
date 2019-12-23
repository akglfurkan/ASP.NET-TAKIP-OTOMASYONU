using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TakipProg.Models
{
    public class LoginModel
    {

        public string username { get; set; }
        public string password { get; set; }
    }

    public class RegisterModel
    {

        public string username { get; set; }
        public string password { get; set; }
        

    }
    public class RoleEditModel
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<ApplicationUser> members { get; set; }
        public IEnumerable<ApplicationUser> NoNmembers { get; set; }
    }

    public class RoleUpdateModel
    {
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
    }
}
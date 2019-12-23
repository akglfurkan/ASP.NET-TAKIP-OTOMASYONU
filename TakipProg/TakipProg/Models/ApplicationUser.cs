using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TakipProg.Models
{
    public class ApplicationUser:IdentityUser
    {
        public List<Calisma> Calismalar { get; set; }


    }
}
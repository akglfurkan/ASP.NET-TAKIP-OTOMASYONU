using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TakipProg.Models
{
    public class DataContext:IdentityDbContext<ApplicationUser>
    {
        public DataContext():base("Connection")
        {
            Database.SetInitializer(new DataInitializer());
        }
        public DbSet<Makina> makinalar { get; set; }
        public DbSet<Calisma> calismalar { get; set; }
        

    }
}
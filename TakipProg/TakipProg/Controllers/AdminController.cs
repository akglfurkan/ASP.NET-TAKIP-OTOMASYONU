using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TakipProg.Models;


namespace TakipProg.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private UserManager<ApplicationUser> userManager1;

        public AdminController()
        {
            var userStore = new UserStore<ApplicationUser>(new DataContext());
            userManager1 = new UserManager<ApplicationUser>(userStore);
        }

        // GET: Admin
        public ActionResult Index()
        {


            return View(userManager1.Users);
        }
    }
}
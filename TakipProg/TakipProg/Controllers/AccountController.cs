using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TakipProg.Models;


namespace TakipProg.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {

        private UserManager<ApplicationUser> userManager;
        public AccountController()
        {
            var userStore = new UserStore<ApplicationUser>(new DataContext());
            userManager = new UserManager<ApplicationUser>(userStore);
        }

        DataContext db = new DataContext();
        // GET: Account
        
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (HttpContext.User.Identity.IsAuthenticated)
            {
                return View("Error", new string[] { "Erisim Hakkınız Yok" });
            }

            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.Find(model.username, model.password);
                if (user == null)
                {
                    ModelState.AddModelError("", "Yanlış kullanıcı adı veya parola.");

                }
                else
                {
                    var authManager = HttpContext.GetOwinContext().Authentication;

                    var identity = userManager.CreateIdentity(user, "ApplicationCookie");

                    var authProperties = new AuthenticationProperties()
                    {
                        IsPersistent = true
                    };

                    authManager.SignOut();
                    authManager.SignIn(authProperties, identity);
                    return RedirectToAction("Index", "Home");
                }

            }

            return View(model);

        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser();

                user.UserName = model.username;


                var result = userManager.Create(user, model.password);

                if (result.Succeeded)
                {
                    userManager.AddToRole(user.Id,"Worker");
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item);
                    }
                }
                
            }


            return View(model);
        }

       
       

        
        
        



        [AllowAnonymous]
        public ActionResult Logout()
        {
            var authManager = HttpContext.GetOwinContext().Authentication;

            authManager.SignOut();
            return RedirectToAction("Login");


        }

    }
}
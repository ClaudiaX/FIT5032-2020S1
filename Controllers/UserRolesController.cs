using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnPeu.Models;

namespace UnPeu.Controllers
{
    public class UserRolesController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        private UserManager<ApplicationUser> UserManager;

        public UserRolesController()
        {
            var userStore = new UserStore<ApplicationUser>(context);
            UserManager = new UserManager<ApplicationUser>(userStore);
        }

        public ActionResult Index()
        {
            return View();
        }

        // GET
        public ActionResult AddRoleToUser()
        {
            var roles = context.Roles.OrderBy(r => r.Name).ToList().Select(r => new SelectListItem { Value = r.Name.ToString(), Text = r.Name }).ToList();
            ViewBag.Roles = roles;
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRoleToUser(string UserName, string RoleName)
        {
            try
            {
                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                this.UserManager.AddToRole(user.Id, RoleName);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET 
        public ActionResult GetUserRoles()
        {
            return View();
        }


        // POST
        [HttpPost]
        public ActionResult GetUserRoles(string UserName)
        {
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            ViewBag.RolesForUser = this.UserManager.GetRoles(user.Id);
            return View();
        }

        [HttpGet]
        public ActionResult DeleteUserRole()
        {
            var roles = context.Roles.OrderBy(r => r.Name).ToList().Select(r => new SelectListItem { Value = r.Name.ToString(), Text = r.Name }).ToList();
            ViewBag.Roles = roles;
            return View();
        }

        [HttpPost]
        public ActionResult DeleteUserRole(string UserName, string RoleName)
        {

            ApplicationUser user = context.Users.Where(u => u.Email.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

            if (this.UserManager.IsInRole(user.Id, RoleName))
            {
                this.UserManager.RemoveFromRole(user.Id, RoleName);
            }
            return View("Index");

        }
    }
}
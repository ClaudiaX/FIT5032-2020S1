using Microsoft.Owin.Security.MicrosoftAccount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnPeu.Models;

namespace UnPeu.Controllers
{
    public class RolesController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

      //  [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            if (User.IsInRole("Admin"))
            {
                var roles = context.Roles.ToList();
                return View(roles);
            }
            else
            {
                return HttpNotFound();
            }
        }

        // GET
        public ActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {

                //var newRole = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                //{
                //    Name = collection["RoleName"]
                //};

                //context.Roles.Add(newRole);


                context.Roles.Add(new Microsoft.AspNet.Identity.EntityFramework.IdentityRole()
                {
                    Name = collection["RoleName"]
                });
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET
        public ActionResult Edit(string roleName)
        {
            var role = context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            return View(role);
        }

        // POST
        [HttpPost]
        public ActionResult Edit(Microsoft.AspNet.Identity.EntityFramework.IdentityRole role)
        {
            try
            {
                context.Entry(role).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(string roleName)
        {
            var role = context.Roles.Where(r => r.Name.Equals(roleName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            context.Roles.Remove(role);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
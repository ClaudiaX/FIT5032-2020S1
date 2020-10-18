using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UnPeu.Models;
using UnPeu.Utils;

namespace UnPeu.Controllers
{
    public class BookEventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: BookEvents
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Customer")]
        public ActionResult Book(int id)
        {
            // id = branch Event Id
            //var user = db.Users.Find(User.Identity.GetUserId());
            var branchEvent = db.BranchEvents.Where(be => be.Id == id).FirstOrDefault();
            //var bookEvent = new BookEvent {ApplicationUser = user,BranchEvent = BranchEvent, }
            var userId = User.Identity.GetUserId();
            var check = db.BookEvents.Where(be => be.BranchEventId == id && be.ApplicationUserId.Equals(userId)).FirstOrDefault();

            if (check == null)
            {

                var bookEvent = new BookEvent { ApplicationUserId = User.Identity.GetUserId(), BranchEventId = id };
                db.BookEvents.Add(bookEvent);
                db.SaveChanges();


                string toEmail = User.Identity.GetUserName();
                string subject = "Book Event Confirm";
                string content = "This is the content of test email. Start time: " + branchEvent.StartTime.ToString();


                EmailSender es = new EmailSender();
                es.SendSingle(toEmail, subject, content);
                // send email
            }
            else
            {
                ViewBag.Error = "You have already bookend this event!";
                // return error page
            }

            return View("index");
        }
    }
}
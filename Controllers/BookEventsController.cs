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



        public JsonResult Analysis()
        {
            var bookEvents = db.BookEvents.Include("BranchEvent").Include("BranchEvent.Branch").Include("BranchEvent.EventType").ToList();

            // eventType     count()

            var bookPerEvent =
                from bookEvent in bookEvents
                group bookEvent by bookEvent.BranchEvent.EventType into bookGroup

                select new
                {
                    Event = bookGroup.Key,
                    Count = bookGroup.Count()
                };

            var bookPerBranchEvent =
                from bookEvent in bookEvents
                group bookEvent by new { bookEvent.BranchEvent.Branch, bookEvent.BranchEvent.EventType } into bookGroup
                select new
                {
                    Book = bookGroup.Key,
                    Count = bookGroup.Count()
                };

            //return new JsonResult { Data = bookPerEvent, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

            return new JsonResult { Data = bookPerBranchEvent, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }
    }
}
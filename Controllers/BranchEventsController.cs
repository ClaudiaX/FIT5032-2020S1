using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using UnPeu.Models;
using UnPeu.Utils;

namespace UnPeu.Controllers
{
    public class BranchEventsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: BranchEvents
        public ActionResult Index()
        {
            var branchEvents = db.BranchEvents.Include(b => b.Branch).Include(b => b.EventType);
            return View(branchEvents.ToList());
        }

        // GET: BranchEvents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BranchEvent branchEvent = db.BranchEvents.Find(id);
            if (branchEvent == null)
            {
                return HttpNotFound();
            }
            return View(branchEvent);
        }

        // GET: BranchEvents/Create
        public ActionResult Create()
        {
            ViewBag.BranchId = new SelectList(db.Branches, "Id", "Name");
            ViewBag.EventTypeId = new SelectList(db.EventTypes, "Id", "Name");
            return View();
        }

        // POST: BranchEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,EventTypeId,BranchId,StartTime")] BranchEvent branchEvent)
        {
            if (ModelState.IsValid)
            {
                db.BranchEvents.Add(branchEvent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BranchId = new SelectList(db.Branches, "Id", "Name", branchEvent.BranchId);
            ViewBag.EventTypeId = new SelectList(db.EventTypes, "Id", "Name", branchEvent.EventTypeId);
            return View(branchEvent);
        }

        // GET: BranchEvents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BranchEvent branchEvent = db.BranchEvents.Find(id);
            if (branchEvent == null)
            {
                return HttpNotFound();
            }
            ViewBag.BranchId = new SelectList(db.Branches, "Id", "Name", branchEvent.BranchId);
            ViewBag.EventTypeId = new SelectList(db.EventTypes, "Id", "Name", branchEvent.EventTypeId);
            return View(branchEvent);
        }

        // POST: BranchEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EventTypeId,BranchId,StartTime")] BranchEvent branchEvent)
        {
            if (ModelState.IsValid)
            {
                var bookevents = db.BookEvents.Include(be => be.ApplicationUser).Where(be => be.BranchEvent.Id == branchEvent.Id).ToList();
                List<string> emailAdd = new List<string>();
                for (int i = 0; i < bookevents.Count; i++)
                {
                    emailAdd.Add(bookevents[i].ApplicationUser.Email);
                    System.Diagnostics.Debug.Write(bookevents[i].ApplicationUser.Email);
                }

                string subject = "Change of event";
                string content = "Event change";

                EmailSender es = new EmailSender();
                es.SendMultiple(emailAdd, subject, content);

                db.Entry(branchEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BranchId = new SelectList(db.Branches, "Id", "Name", branchEvent.BranchId);
            ViewBag.EventTypeId = new SelectList(db.EventTypes, "Id", "Name", branchEvent.EventTypeId);
            return View(branchEvent);
        }

        // GET: BranchEvents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BranchEvent branchEvent = db.BranchEvents.Find(id);
            if (branchEvent == null)
            {
                return HttpNotFound();
            }
            return View(branchEvent);
        }

        // POST: BranchEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BranchEvent branchEvent = db.BranchEvents.Find(id);
            db.BranchEvents.Remove(branchEvent);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET BranchEvents/EventsByBranch/id
        public ActionResult EventsByBranch(int id)
        {
            // id: branchId
            ViewBag.branch = db.Branches.FirstOrDefault(b => b.Id == id);
            var branchEvents = db.BranchEvents.Include(be => be.Branch).Include(be => be.EventType).Where(be => be.BranchId == id).ToList();
            return View(branchEvents);
        }

        // GET BranchEvents/GetEventsByBranch/id
        public JsonResult GetEventsByBranch(int id)
        {
            var branchEvents = db.BranchEvents.Include(be => be.Branch).Include(be => be.EventType).Where(be => be.BranchId == id).ToList();
            return new JsonResult { Data = branchEvents, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;
using Microsoft.AspNet.Identity;


namespace BugTracker.Controllers
{
    public class TicketCommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TicketComments
        //public ActionResult Index()
        //{
        //    var ticketComments = db.TicketComments.Include(t => t.Ticket);
        //    return View(ticketComments.ToList());
        //}


        public ActionResult Index(int Id)
        {
            var ticketComments = db.TicketComments.Select(t => t.TicketId == Id);
            return View(ticketComments.ToList());
        }

        // GET: TicketComments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketComment ticketComment = db.TicketComments.Find(id);
            if (ticketComment == null)
            {
                return HttpNotFound();
            }
            return View(ticketComment);
        }

        // GET: TicketComments/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title");
            return View();
        }

        // POST: TicketComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Comment,TicketId")] TicketComment ticketComment)
        {
            //if (ModelState.IsValid)
            if (true)
            {
                ticketComment.Created = System.DateTime.Now;
                string currentUserId = User.Identity.GetUserId();
                ticketComment.UserId = currentUserId;

                db.TicketComments.Add(ticketComment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

   //         ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title", ticketComment.TicketId);
     //       return View(ticketComment);
        }

        //// GET: TicketComments/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TicketComment ticketComment = db.TicketComments.Find(id);
        //    if (ticketComment == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title", ticketComment.TicketId);
        //    return View(ticketComment);
        //}

        //// POST: TicketComments/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Comment,Created,TicketId,UserId")] TicketComment ticketComment)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Entry(ticketComment).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title", ticketComment.TicketId);
        //    return View(ticketComment);
        //}

        // GET: TicketComments/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    TicketComment ticketComment = db.TicketComments.Find(id);
        //    if (ticketComment == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(ticketComment);
        //}

        //// POST: TicketComments/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    TicketComment ticketComment = db.TicketComments.Find(id);
        //    db.TicketComments.Remove(ticketComment);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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

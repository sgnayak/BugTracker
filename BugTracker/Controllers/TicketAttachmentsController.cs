using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;
using System.IO;
using Microsoft.AspNet.Identity;


namespace BugTracker.Controllers
{
    public class TicketAttachmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TicketAttachments
        public ActionResult Index()
        {
            var ticketAttachments = db.TicketAttachments.Include(t => t.Ticket);
            return View(ticketAttachments.ToList());
        }

        // GET: TicketAttachments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketAttachment ticketAttachment = db.TicketAttachments.Find(id);
            if (ticketAttachment == null)
            {
                return HttpNotFound();
            }
            return View(ticketAttachment);
        }

        // GET: TicketAttachments/Create
        public ActionResult Create(int id)
        {
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title");

            TicketAttachment ticketAttachment = new TicketAttachment();
            ticketAttachment.TicketId = id;
            ticketAttachment.Ticket = db.Tickets.FirstOrDefault(t => t.Id == id);
            var currentUser = User.Identity.GetUserId();
            ticketAttachment.UserId = currentUser;
            ticketAttachment.User = db.Users.FirstOrDefault(u => u.Id == currentUser);
            return View(ticketAttachment);
        }

        // POST: TicketAttachments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TicketId,Decsription")] TicketAttachment ticketAttachment, HttpPostedFileBase fileUpload)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ticketAttachment.UserId = currentUserId;
                ticketAttachment.Created = System.DateTime.Now;

                if (fileUpload != null && fileUpload.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(fileUpload.FileName);
                    fileUpload.SaveAs(Path.Combine(Server.MapPath("~/TicketAttachment/"), fileName));
                    ticketAttachment.FilePath=("~/TicketAttachment/" + fileName);
                }

                db.TicketAttachments.Add(ticketAttachment);
                db.SaveChanges();
                return RedirectToAction("Details", "Tickets", new { id = ticketAttachment.TicketId });
            }


            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title", ticketAttachment.TicketId);
            return View(ticketAttachment);
        }

        // GET: TicketAttachments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketAttachment ticketAttachment = db.TicketAttachments.Find(id);
            if (ticketAttachment == null)
            {
                return HttpNotFound();
            }
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title", ticketAttachment.TicketId);
            return View(ticketAttachment);
        }

        // POST: TicketAttachments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TicketId,FilePath,Decsription,Created,UserId,FileUrl")] TicketAttachment ticketAttachment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticketAttachment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TicketId = new SelectList(db.Tickets, "Id", "Title", ticketAttachment.TicketId);
            return View(ticketAttachment);
        }

        // GET: TicketAttachments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketAttachment ticketAttachment = db.TicketAttachments.Find(id);
            if (ticketAttachment == null)
            {
                return HttpNotFound();
            }
            return View(ticketAttachment);
        }

        // POST: TicketAttachments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketAttachment ticketAttachment = db.TicketAttachments.Find(id);
            db.TicketAttachments.Remove(ticketAttachment);
            db.SaveChanges();
            return RedirectToAction("Index");
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

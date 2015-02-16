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
            [Authorize]
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private UserRolesHelper helperRole = new UserRolesHelper();

        // GET: Tickets
        public ActionResult Index()
        {
            var tickets = db.Tickets.Include(t => t.OwnerUser).Include(t => t.Project).Include(t => t.TicketPriority).Include(t => t.TicketStatus).Include(t => t.TicketType);

            return View(tickets.ToList());
        }


        [HttpPost]
        public ActionResult Search(string searchStr)
        {
            var result = db.Tickets.Where(p => p.Description.Contains(searchStr))
                .Union(db.Tickets.Where(p => p.Title.Contains(searchStr)))
                .Union(db.Tickets.Where(p => p.TicketComments.Any(c => c.Comment.Contains(searchStr))))
                .Union(db.Tickets.Where(p => p.TicketComments.Any(c => c.User.DisplayName.Contains(searchStr))))
                .Union(db.Tickets.Where(p => p.TicketAttachments.Any(c => c.Decsription.Contains(searchStr))))
                .Union(db.Tickets.Where(p => p.Project.Name.Contains(searchStr)))
                .Union(db.Tickets.Where(p => p.OwnerUser.DisplayName.Contains(searchStr)))
                .Union(db.Tickets.Where(p => p.AssignedToUser.DisplayName.Contains(searchStr)))
                .Union(db.Tickets.Where(p => p.TicketPriority.Name.Contains(searchStr)))
                .Union(db.Tickets.Where(p => p.TicketType.Name.Contains(searchStr)))
                .Union(db.Tickets.Where(p => p.TicketStatus.Name.Contains(searchStr)))
                ;
            ViewData["myString"] = searchStr;

            return View("Index",result.ToList());
        }

        // GET: Tickets/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            ticket.TicketRead = true;
            string currentUserId = User.Identity.GetUserId();
            ViewBag.AssignedToUserId = ticket.AssignedToUser.FirstName;

            // If the User is Currently Logged in and Ticket Notification for ticket id belongs to the person remove it
                TicketNotification ticketNotification = new TicketNotification();


                ticketNotification = db.TicketNotifications.FirstOrDefault(u => u.UserId == currentUserId && u.TicketId == ticket.Id);
                if (ticketNotification != null)
                {
                    db.TicketNotifications.Remove(ticketNotification);
                }

            db.Entry(ticket).State = EntityState.Modified;
            db.SaveChanges();
            return View(ticket);
        }

        // GET: Tickets/Create
        [Authorize]
        public ActionResult Create()
        {
            Ticket ticket = new Ticket();

            UserRolesHelper Helper = new UserRolesHelper();
            var devId = db.Roles.Single(r => r.Name == "Developer").Id;
            var adminId = db.Roles.Single(r => r.Name == "Admin").Id;
            var pmId = db.Roles.Single(r => r.Name == "Project Manager").Id;
      //      var devs = ticket.Project.Users.AsQueryable().Where(u => u.Roles.Any(r => r.RoleId == devId || r.RoleId == adminId || r.RoleId == pmId)).ToList();
            var allDevs = db.Users.AsQueryable().Where(u => u.Roles.Any(r => r.RoleId == devId)).ToList();
           
            //ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.AssignedToUserId = new SelectList(allDevs, "Id", "FirstName");
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name");
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name");
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name");
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name");
            return View(ticket);
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId,AssignedToUserId,TicketAttachments")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {

                string currentUserId = User.Identity.GetUserId();
       //         ticket.OwnerUser = db.Users.FirstOrDefault(u => u.Id == currentUserId);
                ticket.OwnerUserId = currentUserId;
                ticket.Created = System.DateTime.Now;
                ticket.TicketRead = false;
                TicketNotification ticketNotification = new TicketNotification();
                ticketNotification.UserId = ticket.AssignedToUserId;
                ticketNotification.TicketId = ticket.Id;
                ticketNotification.TickedNew = true;
                ticketNotification.TicketChanged = false;

                db.TicketNotifications.Add(ticketNotification);
                db.Tickets.Add(ticket);
                db.SaveChanges();


                return RedirectToAction("Index");

            }

            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FirstName", ticket.OwnerUserId);
            ViewBag.AssignedToUserId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);



            return View(ticket);
        }

        // GET: Tickets/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = db.Tickets.Find(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }

            var devId = db.Roles.Single(r => r.Name == "Developer").Id;
            var adminId = db.Roles.Single(r => r.Name == "Admin").Id;
            var pmId = db.Roles.Single(r => r.Name == "Project Manager").Id;
            //      var devs = ticket.Project.Users.AsQueryable().Where(u => u.Roles.Any(r => r.RoleId == devId || r.RoleId == adminId || r.RoleId == pmId)).ToList();
            var allDevs = db.Users.AsQueryable().Where(u => u.Roles.Any(r => r.RoleId == devId)).ToList();


            //ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FirstName");
            ViewBag.AssignedToUserId = new SelectList(allDevs, "Id", "FirstName");
   
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FirstName", ticket.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,ProjectId,TicketTypeId,TicketPriorityId,TicketStatusId,OwnerUserId,AssignedToUserId")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                TicketHistory tHistory = new TicketHistory();
                //TicketHistory lastTicket = ticket.TicketHistories.LastOrDefault();
                //tHistory.OldValue = lastTicket.NewValue;
                //lastTicket.NewValue = ticket.Id;
                //tHistory.NewValue = null;

                Ticket newTicket = new Ticket();
                    newTicket = ticket;
                Ticket lastTicket = new Ticket();
                lastTicket = db.Tickets.AsNoTracking().Single(t => t.Id == ticket.Id);

                // If the Assignment Changes in the Edit Create new Notification else create just history
                if (newTicket.AssignedToUserId != lastTicket.AssignedToUserId)
                {
                    TicketNotification ticketNotification = new TicketNotification();
                    ticketNotification.UserId = ticket.AssignedToUserId;
                    ticketNotification.TicketId = ticket.Id;
                    ticketNotification.TickedNew = false;
                    ticketNotification.TicketChanged = true;
                    db.TicketNotifications.Add(ticketNotification);
                }

                tHistory = addTicketHistories(newTicket, lastTicket);
                tHistory.TicketId = newTicket.Id;
                string currentUserId = User.Identity.GetUserId();
             //   tHistory.User = db.Users.FirstOrDefault(u => u.Id == currentUserId);
                tHistory.UserId = currentUserId;

                ticket.Updated = System.DateTime.Now;

                db.TicketHistories.Add(tHistory);
                db.SaveChanges();

                db.Entry(ticket).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OwnerUserId = new SelectList(db.Users, "Id", "FirstName", ticket.OwnerUserId);
            ViewBag.ProjectId = new SelectList(db.Projects, "Id", "Name", ticket.ProjectId);
            ViewBag.TicketPriorityId = new SelectList(db.TicketPriorities, "Id", "Name", ticket.TicketPriorityId);
            ViewBag.TicketStatusId = new SelectList(db.TicketStatuses, "Id", "Name", ticket.TicketStatusId);
            ViewBag.TicketTypeId = new SelectList(db.TicketTypes, "Id", "Name", ticket.TicketTypeId);
            return View("Edit");
        }

        // GET: Tickets/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Ticket ticket = db.Tickets.Find(id);
        //    if (ticket == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(ticket);
        //}

        //// POST: Tickets/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Ticket ticket = db.Tickets.Find(id);
        //    db.Tickets.Remove(ticket);
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

        private TicketHistory addTicketHistories(Ticket newT, Ticket oldT)
        {
            TicketHistory myTHistory = new TicketHistory();
            if (newT.ProjectId != oldT.ProjectId)
            {
                myTHistory.OldProjectId = oldT.ProjectId;
                myTHistory.OldProjectName = oldT.Project.Name;
                myTHistory.ProjectId = newT.ProjectId;
                myTHistory.ProjectName = db.Projects.FirstOrDefault(p => p.Id == newT.ProjectId).Name;
            }
            if (newT.TicketTypeId != oldT.TicketTypeId)
            {
                myTHistory.OldTicketTypeId = oldT.TicketTypeId;
                myTHistory.OldTicketTypeName = oldT.TicketType.Name;
                myTHistory.TicketTypeId = newT.TicketTypeId;
                myTHistory.TicketTypeName = db.TicketTypes.FirstOrDefault(tp => tp.Id == newT.TicketTypeId).Name;
            }
            if (newT.TicketPriorityId != oldT.TicketPriorityId)
            {
                myTHistory.OldTicketPriorityId = oldT.TicketPriorityId;
                myTHistory.OldTicketPriorityName = oldT.TicketPriority.Name;
                myTHistory.TicketPriorityId = newT.TicketPriorityId;
                myTHistory.TicketPriorityName = db.TicketPriorities.FirstOrDefault(tp => tp.Id == newT.TicketPriorityId).Name;
            }
            if (newT.TicketStatusId != oldT.TicketStatusId)
            {
                myTHistory.OldTicketStatusId = oldT.TicketStatusId;
                myTHistory.OldTicketStatusName = oldT.TicketStatus.Name;
                myTHistory.TicketStatusId = newT.TicketStatusId;
                myTHistory.TicketStatusName = db.TicketStatuses.FirstOrDefault(s => s.Id == newT.TicketStatusId).Name;

            }
            if (newT.AssignedToUserId != oldT.AssignedToUserId)
            {
                myTHistory.OldAssignedToUserId = oldT.AssignedToUserId;
                myTHistory.AssignedToUserId = newT.AssignedToUserId;
            }
            return myTHistory;
        }
    }
}

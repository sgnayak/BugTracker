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

namespace BugTracker.Models
{
    public class NotificationsStatic
    {
        // Can not be used inside static functions
        private ApplicationDbContext _db = new ApplicationDbContext();

        // Up Top Globe Total Notifications
        public static int GetUserNotificationsStatic(string currentUserId)
        {
            int count = 0;
            //string currentUserId = User.Identity.GetUserId();
            ApplicationDbContext _db = new ApplicationDbContext();
            ApplicationUser user = _db.Users.FirstOrDefault(u => u.Id == currentUserId);
            if (user != null)
            {
                count = user.TicketNotifications.Count;
            }
            else
            {
                count = 5;
            }
            return (count);
        }

        // This returns tickets that belong to new notifications that belongs to current user
        public static IList<Ticket> GetNewUserTickets(string currentUserId)
        {
            //string currentUserId = User.Identity.GetUserId();
            ApplicationDbContext _db = new ApplicationDbContext();
            //Nullable<System.DateTimeOffset> lastLogin;

            //var tickets = _db.Tickets.Where(t => t.AssignedToUserId == currentUserId).ToList();
            IList<Ticket> tickets = new List<Ticket>();


            if (!String.IsNullOrWhiteSpace(currentUserId))
            {
                var user = _db.Users.Find(currentUserId);
                if (user != null)
                {
                    //tickets = _db.Tickets.Where(t => t.AssignedToUserId == currentUserId).OrderByDescending(t => t.Created).Take(4).ToList();

                    var query = from tn in _db.TicketNotifications
                                where tn.UserId == currentUserId
                                from t in _db.Tickets
                                where t.Id == tn.TicketId
                                select t;

                    tickets = query.ToList();
                }
            }
            else
            {
                tickets = null;
            }
            return (tickets.ToList());
        }

        // This returns tickets that belong to new notifications that belongs to current user
        // This returns Tickets that were created since last Logon
        public static IList<Ticket> GetNewUserTicketsSinceLastLogOn(string currentUserId)
        {
            //string currentUserId = User.Identity.GetUserId();
            ApplicationDbContext _db = new ApplicationDbContext();
            //Nullable<System.DateTimeOffset> lastLogin;

            IList<Ticket> tickets = new List<Ticket>();

            var lastLogin = _db.Users.FirstOrDefault(u => u.Id == currentUserId).TimeLastLogOn;

            if (!String.IsNullOrWhiteSpace(currentUserId))
            {
                var user = _db.Users.Find(currentUserId);
                if (user != null)
                {
                    tickets = _db.Tickets.Where(t => t.AssignedToUserId == currentUserId).Where(t => t.Created > lastLogin).Take(4).ToList();

                    //var query = from tn in _db.TicketNotifications
                    //            where tn.UserId == currentUserId
                    //            from t in _db.Tickets
                    //            where t.Id == tn.TicketId
                    //            select t;

                    //tickets = query.ToList();
                }
            }
            else
            {
                tickets = null;
            }
            return (tickets.ToList());
        }





        public static IList<Project> GetProjectsForUser(string currentUserId)
        {
            //string currentUserId = User.Identity.GetUserId();
            ApplicationDbContext _db = new ApplicationDbContext();
            //Nullable<System.DateTimeOffset> lastLogin;

            //var tickets = _db.Tickets.Where(t => t.AssignedToUserId == currentUserId).ToList();
            IList<Project> projects = new List<Project>();


            if (!String.IsNullOrWhiteSpace(currentUserId))
            {
                var user = _db.Users.Find(currentUserId);
                if (user != null)
                {

                    var query = from u in _db.Users
                                where u.Id == currentUserId
                                from pr in u.Projects
                                select pr
                               ;

                    projects = query.ToList();
                    //     return (query.ToList());

                }
            }
            else
            {
                projects = null;
            }
            return (projects);
        }

        public static IList<TicketComment> GetUserComments(string currentUserId)
        {

            ApplicationDbContext _db = new ApplicationDbContext();
            //Nullable<System.DateTimeOffset> lastLogin;

            //var tickets = _db.Tickets.Where(t => t.AssignedToUserId == currentUserId).ToList();
            IList<TicketComment> comments = new List<TicketComment>();
            var lastLogin = _db.Users.FirstOrDefault(u => u.Id == currentUserId).TimeLastLogOn;


            if (!String.IsNullOrWhiteSpace(currentUserId))
            {
                var user = _db.Users.Find(currentUserId);
                if (user != null)
                {

                    if (lastLogin != null)
                    {
                        var currentUserComments = _db.TicketComments.Where(u => u.Ticket.AssignedToUserId == currentUserId);
                        if (currentUserComments != null)
                        {
                            comments = currentUserComments.Where(t => t.Created > lastLogin).ToList();
                        }
                    }
                    else
                    {
                        comments = null;
                    }

                    //var query = from u in _db.Users
                    //            where u.Id == currentUserId
                    //            from cm in u.TicketComments.OrderByDescending(c => c.Created > lastLogin).Take(4)
                    //            select cm
                    //           ;

                    //comments = query.ToList();
                    //     return (query.ToList());

                }
            }
            else
            {
                comments = null;
            }
            return (comments);

        }

        public int GetUserProjectsCount(string currentUserId)
        {
            int count = 0;
            //string currentUserId = User.Identity.GetUserId();
            if (currentUserId != null)
            {
                var userProjects = _db.Users.FirstOrDefault(u => u.Id == currentUserId);
                if (userProjects != null)
                {
                    count = userProjects.Projects.Count();
                }
            }
            else
            {
                count = 5;
            }
            return (count);
        }

        public string GetUserRole(string currentUserId)
        {
            UserRolesHelper uRoleHelper = new UserRolesHelper();
            string role = "Submitter";
            //string currentUserId = User.Identity.GetUserId();
            if (currentUserId != null)
            {
                var roles = uRoleHelper.ListUserRoles(currentUserId).OrderBy(s => s);
                foreach (string myRole in roles)
                {
                    if (myRole == "Admin")
                    {
                        role = "Admin";
                        break;
                    }
                    else if (myRole == "Developer")
                    {
                        role = "Developer";
                        break;
                    }
                    else if (myRole == "Project Manager")
                    {
                        role = "Manager";
                        break;
                    }
                    else
                    {
                        role = "Submitter";
                        break;
                    }
                }
            }
            else
            {
                role = "Submitter";
            }

            return (role);
        }

        public List<Ticket> GetRecentTickets()
        {
            //  Ticket ticket = new Ticket;
            //string currentUserId = User.Identity.GetUserId();
            //var ticketsForCurrentUser = _db.Tickets.Include(u => u.OwnerUserId == currentUserId).ToList();
            //var tickets = ticketsForCurrentUser.OrderByDescending(u => u.Created).Take(10).ToList();
            var tickets = _db.Tickets.OrderByDescending(u => u.Created).Take(10).ToList();
            return tickets;
        }

    }
}
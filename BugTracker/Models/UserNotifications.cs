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
    public class UserNotifications : Controller
    {

        private ApplicationDbContext _db = new ApplicationDbContext();

        public int GetUserNotifications(string currentUserId)
        {
            int count = 0;
            //string currentUserId = User.Identity.GetUserId();
            if (currentUserId != null)
            {
                    count = _db.TicketNotifications.Select(u => u.UserId == currentUserId).Count();
            }
            else
            {
                count = 5;
            }
            return (count);
        }

        public int GetNewUserTicketCount(string currentUserId)
        {
            int count = 0;
            //string currentUserId = User.Identity.GetUserId();
            Nullable<System.DateTimeOffset> lastLogin;

            if (!String.IsNullOrWhiteSpace(currentUserId))
            {
                var user = _db.Users.Find(currentUserId);

                if (user != null)
                {
                    lastLogin = user.TimeLastLogOn;

                    var tickets = user.Tickets;

                    if (lastLogin != null)
                    {
                        if (tickets.Count > 0)
                            count = tickets.Where(t => t.Created > lastLogin).Count();
                    }
                }
            }
            else
            {
                count = 25;
            }
            return (count);
        }

        public int GetNewUserCommentsCount(string currentUserId)
        {
            int count = 0;
            //string currentUserId = User.Identity.GetUserId();
            Nullable<System.DateTimeOffset> lastLogin;

            if (currentUserId != null)
            {
                if ((_db.Users.FirstOrDefault(u => u.Id == currentUserId)) != null)
                {
                    lastLogin = _db.Users.FirstOrDefault(u => u.Id == currentUserId).TimeLastLogOn;

                    if (lastLogin != null)
                    {
                        var currentUserComments = _db.TicketComments.Where(u => u.Ticket.AssignedToUserId == currentUserId);
                        if (currentUserComments != null)
                        {
                            count = currentUserComments.OrderByDescending(t => t.Created > lastLogin).Count();
                        }
                    }
                }
            }
            else
            {
                count = 25;
            }
            return (count);
        }

        public int GetUserProjectsCount(string currentUserId)
        {
            int count = 0;
            //string currentUserId = User.Identity.GetUserId();
            if (currentUserId != null)
            {
                var userProjects = _db.Users.FirstOrDefault(u => u.Id == currentUserId);
                    if(userProjects != null)
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
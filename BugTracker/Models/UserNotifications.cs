﻿using System;
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
            int count = 21;
            //string currentUserId = User.Identity.GetUserId();
            if (currentUserId != null)
            {
                if (true)
                {
                    count = 23;
                }
                else
                {
                    count = _db.TicketNotifications.Select(u => u.UserId == currentUserId).Count();
                }

            }
            else
            {
                count = 25;
            }
            return (count);
        }

        public int GetNewUserTicketCount(string currentUserId)
        {
            int count = 21;
            //string currentUserId = User.Identity.GetUserId();
            if (currentUserId != null)
            {
                if (true)
                {
                    count = 23;
                }
                else
                {
                    count = _db.TicketNotifications.Select(u => u.UserId == currentUserId).Count();
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
            int count = 21;
            //string currentUserId = User.Identity.GetUserId();
            if (currentUserId != null)
            {
                if (true)
                {
                    count = 23;
                }
                else
                {
                    count = _db.TicketNotifications.Select(u => u.UserId == currentUserId).Count();
                }

            }
            else
            {
                count = 25;
            }
            return (count);
        }

        public string GetUserRole(string currentUserId)
        {
            string role = "Admin";
            //string currentUserId = User.Identity.GetUserId();
            if (currentUserId != null)
            {
                if (true)
                {
                    role = "Admin";
                }
                else
                {
                    role = "Admin";
                }

            }
            else
            {
                role = "Admin";
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
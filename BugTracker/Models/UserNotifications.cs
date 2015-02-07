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
    public class UserNotifications
    {

        private ApplicationDbContext _db;

        public int GetUserNotifications(string currentUserId)
        {
            //string currentUserId = User.Identity.GetUserId();
            int count = _db.TicketNotifications.Count(u => u.UserId == currentUserId);
            return (count);
        }

    }
}
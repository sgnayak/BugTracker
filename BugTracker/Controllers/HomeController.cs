﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using BugTracker.Models;


namespace BugTracker.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Tour()
        {
            ViewBag.Message = "Your application Tour.";

            return View();
        }

        public ActionResult Index()
        {
            UserNotifications userNote = new UserNotifications();
            string currentUserId = User.Identity.GetUserId();
            ViewBag.Name = "Layout Notifications";
            var notCount = userNote.GetUserNotifications(currentUserId);
            ViewBag.NotificationCount = notCount == null ? 0 : notCount;
            ViewBag.ProjectsCount = userNote.GetUserProjectsCount(currentUserId);
            ViewBag.NewTicketCount = userNote.GetNewUserTicketCount(currentUserId);
            ViewBag.NewComments = userNote.GetNewUserCommentsCount(currentUserId);

            ViewBag.Role = userNote.GetUserRole(currentUserId);
            var tickets = userNote.GetRecentTickets();
            return View(tickets);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}
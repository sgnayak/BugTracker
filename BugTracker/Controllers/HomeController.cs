using System;
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

        [Authorize]
        public ActionResult Index()
        {
            UserNotifications userNote = new UserNotifications();
            string currentUserId = User.Identity.GetUserId();
            ViewBag.Name = "Layout Notifications";
            var notCount = userNote.GetUserNotifications(currentUserId);
            ViewBag.NotificationCount = notCount;
            ViewBag.ProjectsCount = userNote.GetUserProjectsCount(currentUserId);
            ViewBag.NewTicketCount = userNote.GetNewUserTicketCount(currentUserId);
            ViewBag.NewComments = userNote.GetNewUserCommentsCount(currentUserId);

            ViewBag.Role = userNote.GetUserRole(currentUserId);
            var tickets = userNote.GetRecentTickets(currentUserId);
            return View(tickets);
        }


    }
}
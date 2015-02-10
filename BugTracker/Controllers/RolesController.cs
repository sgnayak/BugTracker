using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections;

namespace BugTracker.Controllers
{
    public class RolesController : Controller
    {
        // GET: Rolles
        [HttpGet]
        [AllowAnonymous]
        //        [ValidateAntiForgeryToken]
        public ActionResult ListUsers()
        {
            var roles = new List<UserRoleViewModel>();

            ApplicationDbContext db = new ApplicationDbContext();

            var adminId = db.Roles.Single(r => r.Name == "Admin").Id;
            var projectManagerId = db.Roles.Single(r => r.Name == "Project Manager").Id;
            var developerId = db.Roles.Single(r => r.Name == "Developer").Id;
            var SubmitterId = db.Roles.Single(r => r.Name == "Submitter").Id;

            foreach (var user in db.Users.ToList())
            {
                var urvm = new UserRoleViewModel
                {
                    userId = user.Id,
                    name = user.DisplayName,
                    admin = user.Roles.Any(r => r.RoleId == adminId),
                    projectManager = user.Roles.Any(r => r.RoleId == projectManagerId),
                    developer = user.Roles.Any(r => r.RoleId == developerId),
                    submitter = user.Roles.Any(r => r.RoleId == SubmitterId)
                };
                roles.Add(urvm);
            }

            return View(roles);
        }


        [HttpPost]
        //public ActionResult Index(string AdminSelected, string DeveloperSelected, string ProjectManagerSelected, string SubmitterSelected, string UserId, string UserName)
        public ActionResult Index(RolesViewModels model)
        {
            var testRole = model;
            //      ViewBag.Message = "The value of Select is " + AdminSelected + DeveloperSelected + ProjectManagerSelected + SubmitterSelected ?? "";
            return View();
        }


        [HttpGet]
        public ActionResult Edit(string UserId)
        {
            ApplicationDbContext UserDb = new ApplicationDbContext();
            IList<IdentityRole> userRoles = UserDb.Roles.ToList();
            var helper = new UserRolesHelper();
            var ListOfRoles = UserDb.Roles.OrderBy(r => r.Name).ToList().Select(rr => rr.Name.ToString()).ToList();

            RolesViewModels myRole = new RolesViewModels();
            myRole.UserId = UserId;
            var user = UserDb.Users.Find(UserId);
            myRole.UserName = user.Email;

            var ListOfCurrentRoles = helper.ListUserRoles(UserId);

            IList<Roles> allRoles = new List<Roles>();
            foreach (var role in ListOfCurrentRoles)
            {
                if (role == "Admin") { myRole.AdminRoleSelected = true; }
                if (role == "Developer") { myRole.DeveloperRoleSelected = true; }
                if (role == "Project Manager") { myRole.ProjectManagerRoleSelected = true; }
                if (role == "Submitter") { myRole.SubmitterRoleSelected = true; }

                bool roleEn = helper.IsUserInRole(UserId, role);
                Roles myRoles = new Roles(role, roleEn);
                //if (helper.IsUserInRole(user.Id, role)) { myRoles = new Roles(role, true); }
                //else { myRoles = new Roles(role, false); }
                allRoles.Add(myRoles);
            }
            return View(myRole);
        }

        [HttpPost]
        public ActionResult Edit(string AdminSelected, string DeveloperSelected, string ProjectManagerSelected, string SubmitterSelected, string UserId)
        {
            var testRole = UserId;
            var helper = new UserRolesHelper();

            if (AdminSelected == "on")
            {
                helper.AddUserRole(UserId, "Admin");
            }
            else
            {
                helper.RemoveUserRole(UserId, "Admin");
            }
            if (DeveloperSelected == "on")
            {
                helper.AddUserRole(UserId, "Developer");
            }
            else
            {
                helper.RemoveUserRole(UserId, "Developer");
            }
            if (ProjectManagerSelected == "on")
            {
                helper.AddUserRole(UserId, "Project Manager");
            }
            else
            {
                helper.RemoveUserRole(UserId, "Project Manager");
            }
            if (SubmitterSelected == "on")
            {
                helper.AddUserRole(UserId, "Submitter");
            }
            else
            {
                helper.RemoveUserRole(UserId, "Submitter");
            }

            return RedirectToAction("ListUsers");
            //          return View("ListUsers");
        }


        public ActionResult ChooseRolePartial(string username)
        {
            var helper = new UserRolesHelper();
            ApplicationDbContext UserDb = new ApplicationDbContext();
            var ListOfRoles = UserDb.Roles.OrderBy(r => r.Name).ToList().Select(rr => rr.Name.ToString()).ToList();

            var userRoles = helper.ListUserRoles(username);
            var roles = ListOfRoles.Select(x => new SelectListItem
            {
                Value = x,
                Text = x,
                Selected = userRoles.Contains(x)
            }).ToArray();

            var model = new ChooseRoleModel { Roles = roles, Username = username };

            return PartialView("Partials/ChooseRolePartial", model);
        }

        [HttpPost]
        public ActionResult ChooseRolePartial(ChooseRoleModel model)
        {
            var testReturnValue = model;
            return View("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditUserRoles(List<UserRoleViewModel> users)
        {
            UserRolesHelper uRoleHelper = new UserRolesHelper();
            var usersInAdminRole = uRoleHelper.UsersInRoles("Admin");
            var adminCount = usersInAdminRole.Count();

            foreach (var c in users)
            {
                var helper = new UserRolesHelper();
                if (c.admin)
                {
                    helper.AddUserRole(c.userId, "Admin");
                }
                else
                {
                    if (adminCount > 0)
                    {
                        helper.RemoveUserRole(c.userId, "Admin");
                        adminCount--;
                    }
                }
                if (c.projectManager)
                {
                    helper.AddUserRole(c.userId, "Project Manager");
                }
                else
                {
                    helper.RemoveUserRole(c.userId, "Project Manager");
                }
                if (c.developer)
                {
                    helper.AddUserRole(c.userId, "Developer");
                }
                else
                {
                    helper.RemoveUserRole(c.userId, "Developer");
                }
                if (c.submitter)
                {
                    helper.AddUserRole(c.userId, "Submitter");
                }
                else
                {
                    helper.RemoveUserRole(c.userId, "Submitter");
                }
            }
            return RedirectToAction("ListUsers");
        }
    }
}


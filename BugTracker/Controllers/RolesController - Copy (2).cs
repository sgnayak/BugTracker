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
        public ActionResult Index()
        {
            var roles = new List<RolesViewModels>();

            ApplicationDbContext UserDb = new ApplicationDbContext();

            IList<IdentityRole> userRoles = UserDb.Roles.ToList();

            var userList = new List<ApplicationUser>();

            var helper = new UserRolesHelper();

            var selectListOfRoles = UserDb.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            // same as above but just string array than selected list array
            var ListOfRoles = UserDb.Roles.OrderBy(r => r.Name).ToList().Select(rr => rr.Name.ToString()).ToList();


            //    var listOfRoles = UserDb.Roles.OrderBy(r => r.Name).ToList().Select(rr => rr.Name.ToString());


            foreach (var user in UserDb.Users)
            {
                var currentRole = new RolesViewModels();

                currentRole.UserId = user.Id;  // will be hidden field in the View

                if (user.DisplayName == null || user.DisplayName == "")
                {
                    if (user.FirstName == null || user.FirstName == "")
                    {
                        currentRole.UserName = user.Email;
                    }
                    else
                    {
                        currentRole.UserName = user.FirstName + " " + user.LastName;
                    }
                }
                else
                {
                    currentRole.UserName = user.DisplayName;
                }

                IList<Roles> allRoles = new List<Roles>();
                foreach (var role in ListOfRoles)
                {
                    bool roleEn = helper.IsUserInRole(user.Id, role);
                    Roles myRoles = new Roles(role, roleEn);
                    //if (helper.IsUserInRole(user.Id, role)) { myRoles = new Roles(role, true); }
                    //else { myRoles = new Roles(role, false); }
                    allRoles.Add(myRoles);
                }

                currentRole.Roles = allRoles;
                //    currentRole.AvailableRoles = ListOfRoles;
                //helper.ListUserRoles(user.Id)
                //    currentRole.SelectedRoles.ToDictionary(x => x);

                roles.Add(currentRole);

            }

            //            IList<IdentityRole> userRoles = UserDb.Roles.ToList();

            //          var roleslist = userRoles.
            ViewBag.Roles = from s in selectListOfRoles orderby s ascending select s;
            ViewBag.ListOfAvailableRolesString = from s in ListOfRoles orderby s ascending select s;
            return View("Index", roles);
        }




        //       [HttpPost]
        public ActionResult ModifyRoles(RolesViewModels postedRoles)
        {
            //            IList<string> roles = postedRoles; //the selected roles are here
            var postedReturn = postedRoles;

            return View("Index", postedReturn);

            //....
        }


    }
}


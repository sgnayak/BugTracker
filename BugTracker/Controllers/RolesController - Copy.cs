using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections;
using MvcCheckBoxList.Model;

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
                    currentRole.UserName = user.FirstName + " " + user.LastName;
                }
                else
                {
                    currentRole.UserName = user.DisplayName;
                }


                int i = 0;
                IList<myRoles> localUserRoles = new List<myRoles>();
                foreach (var role in helper.ListUserRoles(user.Id))
                {                   
                    var myRoles = new myRoles(i, role, true);
                    localUserRoles.Add(myRoles);
 //                   currentRole.myCurrentRoles.Add(myRoles); 
                    i++;
                }

                i = 0;
                IList<myRoles> localAllRoles = new List<myRoles>();
                foreach (var role in ListOfRoles)
                {
                    var myRoles = new myRoles(i, role, true);
                    localAllRoles.Add(myRoles);
                    i++;
                }
                currentRole.SelectedRoles = localUserRoles;
                currentRole.AvailableRoles = localAllRoles;
 //               currentRole.myCurrentRoles. = helper.ListUserRoles(user.Id);
                roles.Add(currentRole);
                //              var listOfRoles = UserDb.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
                //               ViewBag.Roles = listOfRoles;

            }

            //            IList<IdentityRole> userRoles = UserDb.Roles.ToList();

            //          var roleslist = userRoles.
            ViewBag.Roles = selectListOfRoles;
            ViewBag.ListOfAvailableRolesString = ListOfRoles;
            return View("Index", roles);
        }




 //       [HttpPost]
        public ActionResult ModifyRoles(PostedRoles  postedRoles)
        {
//            IList<string> roles = postedRoles; //the selected roles are here
            var postedReturn = postedRoles;

            return View("Index",GetRolesModel(postedRoles));

            //....
        }

        private RolesViewModels GetRolesModel(PostedRoles postedRoles)
        {
            // setup properties
            var model = new RolesViewModels();
            var selectedRoles = new List<myRoles>();
            var postedRoleIds = new string[0];
            if (postedRoles == null) postedRoles = new PostedRoles();

            // if a view model array of posted Roles ids exists
            // and is not empty,save selected ids
            if (postedRoles.rolesName != null && postedRoles.rolesName.Any())
            {
                postedRoleIds = postedRoles.rolesName;
            }

            // if there are any selected ids saved, create a list of Roles
            //if (postedRoleIds.Any())
            //{
            //    selectedRoles = RoleRepository.GetAll()
            //     .Where(x => postedRoleIds.Any(s => x.Id.ToString().Equals(s)))
            //     .ToList();
            //}

            //setup a view model
            //model.AvailableRoles = RoleRepository.GetAll().ToList();
            model.AvailableRoles = selectedRoles;
            model.SelectedRoles = selectedRoles;
            model.PostedRoles = postedRoles;
            return model;
        }
    }
}


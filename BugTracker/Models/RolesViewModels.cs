namespace BugTracker.Models
{
    using System.Data.Entity;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public partial class RolesViewModels
    {
        //       public ApplicationUser myCurrentUser {get; set;}
        public string UserName;
        public string UserId;
        //        public IList<IdentityUserRole>    myCurrentRoles { get; set; }
        //       public IList<string> AvailableRoles { get; set; }
        public IEnumerable<Roles> Roles { get; set; } // Selected

        public string AdminRole;
        public bool AdminRoleSelected = false;
        public string DeveloperRole;
        public bool DeveloperRoleSelected = false;
        public string ProjectManagerRole;
        public bool ProjectManagerRoleSelected = false;
        public string SubmitterRole;
        public bool SubmitterRoleSelected = false;
 
        //       public PostedRoles PostedRoles { get; set; }
        //public RolesViewModels()
        //{
        //    Roles = Roles.GetRoles().roleIsSelect(r => new Roles()
        //    {
        //        RoleName = r,
        //        roleIsSelected = Roles.GetRolesForUser(uvm.UserProfile.UserName).Contains(r) ? true : false
        //    });
        //}
    }
    public partial class PostedRoles
    {

        public string[] rolesName { get; set; }
    }

    // Defines Roles All Roles and other parameters for checkbox list
    public partial class Roles
    {
        public string RoleName { get; set; }
        public bool RoleIsSelected { get; set; }

        public Roles(string roleName, bool roleIsSelected)
        {
            RoleName = roleName;
            RoleIsSelected = roleIsSelected;
        }


    }

    public class ChooseRoleModel
    {
        public SelectListItem[] Roles { get; set; }
        public string Username { get; set; }
    }


    public class UserRoleViewModel
    {
        public string name { get; set; }
        public string userId { get; set; }
        public bool admin { get; set; }
        public bool developer { get; set; }
        public bool projectManager { get; set; }
        public bool submitter { get; set; }
    }
}
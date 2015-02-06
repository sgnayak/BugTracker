namespace BugTracker.Models
{
    using System.Data.Entity;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public partial class ProjectViewModels
    {
        //       public ApplicationUser myCurrentUser {get; set;}
        public string UserName;
        public string UserId;
        //        public IList<IdentityUserRole>    myCurrentProject { get; set; }
        //       public IList<string> AvailableProject { get; set; }
        public IList<Project> Projects { get; set; } // Selected
        public IList<string> Role;
        //public IEnumerable<ApplicationUser> Users { get; set; }
    }

    public partial class ProjectToUser
    {
        //       public ApplicationUser myCurrentUser {get; set;}
        public string UserName;
        public string UserId;
        //        public IList<IdentityUserRole>    myCurrentProject { get; set; }
        //       public IList<string> AvailableProject { get; set; }

        public MultiSelectList availableProjects { get; set; }
        public string[] selectedProjects { get; set; }

        // User is already in these projects
        public MultiSelectList assignedProjects { get; set; }
        public string[] selectedAssignedProjects { get; set; }

        // User is not in these projects
        public MultiSelectList notAssignedProjects { get; set; }
        public string[] selectedNotAssignedProjects { get; set; }  
    }
}
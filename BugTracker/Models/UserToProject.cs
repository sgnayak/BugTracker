namespace BugTracker.Models
{
    using System.Data.Entity;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.Web.Mvc;


    public partial class UserToProject
    {
        //       public ApplicationUser myCurrentUser {get; set;}
        public string ProjectName;
        public int ProjectId;
        //        public IList<IdentityUserRole>    myCurrentProject { get; set; }
        //       public IList<string> AvailableProject { get; set; }

        public MultiSelectList availableProjects { get; set; }
        public string[] selectedProjects { get; set; }

        // Users already in these projects
        public MultiSelectList usersOnProject { get; set; }
        public string[] selectedUsersOnProject { get; set; }

        // User is not in these projects
        public MultiSelectList usersNotOnProjects { get; set; }
        public string[] selectedUsersNotOnProjects { get; set; }
    }
}
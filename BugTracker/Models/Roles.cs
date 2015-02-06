namespace BugTracker.Models
{
    using System.Data.Entity;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;

    public partial class Roles
    {
 //       public ApplicationUser myCurrentUser {get; set;}
        public string UserName;
        public string UserId;
//        public IList<IdentityUserRole>    myCurrentRoles { get; set; }
        public IList<string> myCurrentRoles { get; set; }
    }
}
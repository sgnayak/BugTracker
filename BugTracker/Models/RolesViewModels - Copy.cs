namespace BugTracker.Models
{
    using System.Data.Entity;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;

    public partial class RolesViewModels
    {
 //       public ApplicationUser myCurrentUser {get; set;}
        public string UserName;
        public string UserId;
//        public IList<IdentityUserRole>    myCurrentRoles { get; set; }
        public IList<myRoles> AvailableRoles { get; set; }
        public IList<myRoles> SelectedRoles { get; set; } // Selected
        public PostedRoles PostedRoles { get; set; }
    }

    public partial class PostedRoles
    {

        public string[] rolesName { get; set; }
    }

    // Defines Roles All Roles and other parameters for checkbox list
    public partial class myRoles
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public object Tags { get; set; }
        public bool IsSelected { get; set; }

        public myRoles(int id, string name, bool isSelected)
        {
            Id = id;
            Name = name;
            IsSelected = isSelected;
        }


    }

    public static class RolesRepository
    {
        /// <summary>
        /// for get Roles for specific id
        /// </summary>
        //public static myRoles Get(int id)
        //{
        //    return GetAll().FirstOrDefault(x => x.Id.Equals(id));
        //}

        /// <summary>
        /// for get all Roless
        /// </summary>
        //public static IEnumerable<myRoles> GetAll()
        //{
        //    return new List<myRoles> {
        //        //new myRoles { Id = 1, Name = "Apple", IsSelected = true },
        //        //              new myRoles {Name = "Banana", Id = 2, IsSelected = true },
        //        //              new myRoles {Name = "Cherry", Id = 3}, IsSelected = true ,
        //        //              new myRoles { Name = "Pineapple", Id = 4, IsSelected = true },
        //        //              new myRoles { Name = "Grape", Id = 5, IsSelected = true },
        //        //              new myRoles { Name = "Guava", Id = 6, IsSelected = true },
        //        //              new myRoles {Name = "Mango", Id = 7, IsSelected = true }
        //        //            };
        //}
    }
}
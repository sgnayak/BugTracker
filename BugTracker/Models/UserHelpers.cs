using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Linq;
using System;

namespace BugTracker.Models
{
    public class UserRolesHelper
    {
        private UserManager<ApplicationUser> manager =
            new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(
                    new ApplicationDbContext()));

        // Ronny Overby Code *********************
        private ApplicationDbContext _db;

        public void DemoGettingUsersInSpecificRole(ApplicationDbContext dbContext)
        {
            if (dbContext == null) throw new ArgumentNullException("dbContext");
            _db = dbContext;
        }
        // Ronny Overby Code *********************



        public bool IsUserInRole(string userId, string roleName)
        {
            return (manager.IsInRole(userId, roleName));
        }

        public IList<string> ListUserRoles(string userId)
        {
            string[] returnSub = {"Submitter"};
            if (userId == null)
            {
                return (returnSub);
            }
            else
            {

                return (returnSub);

    //            return (manager.GetRoles(userId));
            }
        }

        public bool AddUserRole(string userId, string roleName)
        {
            var result = manager.AddToRole(userId, roleName);
            return result.Succeeded;
        }

        public bool RemoveUserRole(string userId, string roleName)
        {
            var result = manager.RemoveFromRole(userId, roleName);
            return result.Succeeded;
        }
        public IList<ApplicationUser> UsersInRoles(string roleName)
        {
            var resultList = new List<ApplicationUser>();
            foreach (var user in manager.Users)
            {
                if (IsUserInRole(user.Id, roleName))
                {
                    resultList.Add(user);
                }
            }
            return (resultList);
        }


        // Ronny Overby Code *********************
        public IList<ApplicationUser> GetUsersInRole(string roleName)
        {
            var query =
                from r in _db.Roles
                where r.Name == roleName
                from ur in r.Users
                join u in _db.Users on ur.UserId equals u.Id
                select u;

            var users = query.ToList();
            return users;
        }
        // Ronny Overby Code *********************


        public IList<ApplicationUser> UsersNotInRoles(string roleName)
        {
            var resultList = new List<ApplicationUser>();
            foreach (var user in manager.Users)
            {
                if (!IsUserInRole(user.Id, roleName))
                {
                    resultList.Add(user);
                }
            }
            return (resultList);
        }

    }

    // ****************************************************************************************************************

    public class UserProjectHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public bool IsOnProject(string userId, int projectId)
        {
            if (db.Users.Any(u => u.Id == userId && u.Projects.Any(p => p.Id == projectId)))
            {
                return true;
            }
            return false;
        }

        public void AddUserToProject(string userId, int projectId)
        {
            // add a 
            if (!this.IsOnProject(userId, projectId))
            {
                var prj = db.Projects.FirstOrDefault(p => p.Id == projectId);
                db.Users.FirstOrDefault(u => u.Id == userId).Projects.Add(prj);
                db.SaveChanges();
            }
        }

        public void RemoveUserFromProject(string userId, int projectId)
        {
            // Remove a user from the project
            if (this.IsOnProject(userId, projectId))
            {
                // var pu = new ProjectUser { ProjectId = projectId, UserId = userId };
                // returns a Task object (Because it is aync method)and it contains the result
                var prj = db.Projects.FirstOrDefault(p => p.Id == projectId);
                db.Users.FirstOrDefault(u => u.Id == userId).Projects.Remove(prj);
                db.SaveChanges();
            }
        }

        public IList<ApplicationUser> UsersOnProject(int projectId)
        {
            var userList = new List<ApplicationUser>();

            userList = db.Projects.FirstOrDefault(p => p.Id == projectId).Users.ToList();

            return userList;
        }

        //public IList<ApplicationUser> UsersOnProject(int projectId)
        //{
        //    var userList = new List<ApplicationUser>();
        //    var rolesHelper = new UserRolesHelper();

        //    foreach (var user in db.Users)
        //    {
        //        if(this.IsOnProject(user.Id, projectId) && rolesHelper.IsUserInRole(user.Id, roleName))
        //        {
        //            userList.Add(user);
        //        }
        //    }
        //    return userList;
        //}

        public IList<ApplicationUser> UsersNotOnProject(int projectId)
        {
            var userList = new List<ApplicationUser>();

            var userList1 = db.Users.Select(u => u.Projects.Select((p => p.Id != projectId))).ToList();
            //foreach (var user in db.Users)
            //{
            //    if (!this.IsOnProject(user.Id, projectId) && rolesHelper.IsUserInRole(user.Id, roleName))
            //    {
            //        userList.Add(user);
            //    }
            //}
            return userList;
        }


        public IList<Project> ListProjectsAssignedToUser(string userId)
        {
            var projectList = new List<Project>();

            //       var user1 = db.Users.FirstOrDefault(u => u.Id == userId);
            var query =
                    from u in db.Users
                    where u.Id == userId
                    from pr in u.Projects
                    //      join pr in db.Users on u.UserId equals u.Id
                    select pr;

            projectList = query.ToList();

            return projectList;
        }

        public IList<Project> ListProjectsNotAssignedToUser(string userId)
        {
            var projectList = new List<Project>();

            //       var user1 = db.Users.FirstOrDefault(u => u.Id == userId);
            var query =
                    from pr in db.Projects
                    where pr.Users.FirstOrDefault().Id != userId
                    select pr;

            projectList = query.ToList();

            return projectList;
        }
    }//class UserProjectHelper






}// Namespace

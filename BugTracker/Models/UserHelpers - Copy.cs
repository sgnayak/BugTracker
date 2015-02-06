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
            return (manager.GetRoles(userId));
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
            if (db.ProjectUsers.Any(p => p.ProjectId == projectId && p.UserId == userId))
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
                var pu = new ProjectUser { ProjectId = projectId, UserId = userId };
                db.ProjectUsers.Add(pu);
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
                var pu = db.ProjectUsers.SingleAsync(p => p.ProjectId == projectId && p.UserId == userId);
                db.ProjectUsers.Remove(pu.Result);
                db.SaveChanges();
            }
        }

        public IList<ApplicationUser> UsersOnProject(int projectId)
        {
            var userList = new List<ApplicationUser>();
            foreach (var user in db.Users)
            {
                if (this.IsOnProject(user.Id, projectId))
                {
                    userList.Add(user);
                }
            }
            return userList;
        }

        public IList<ApplicationUser> UsersOnProject(int projectId, string roleName)
        {
            var userList = new List<ApplicationUser>();
            var rolesHelper = new UserRolesHelper();

            foreach (var user in db.Users)
            {
                if(this.IsOnProject(user.Id, projectId) && rolesHelper.IsUserInRole(user.Id, roleName))
                {
                    userList.Add(user);
                }
            }
            return userList;
        }

        public IList<ApplicationUser> UsersNotOnProject(int projectId, string roleName)
        {
            var userList = new List<ApplicationUser>();
            var rolesHelper = new UserRolesHelper();

            foreach (var user in db.Users)
            {
                if (!this.IsOnProject(user.Id, projectId) && rolesHelper.IsUserInRole(user.Id, roleName))
                {
                    userList.Add(user);
                }
            }
            return userList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">String Id field of Application User</param>
        /// <returns></returns>
        public  IList<Project> ListProjectsForUser(string userId)
        {
            var projectList = new List<Project>();
            foreach (var project in db.Projects)
            {
                if ( this.IsOnProject(userId, project.Id))
                {
                    projectList.Add(project);
                }

            }
            return projectList;
        }

        public IList<Project> ListProjectsNotAssignedToUser(string userId)
        {
            var projectList = new List<Project>();
            foreach (var project in db.Projects)
            {
                if (!this.IsOnProject(userId, project.Id))
                {
                    projectList.Add(project);
                }

            }
            return projectList;
        }
    }//class UserProjectHelper

    // ********************** Project Manager ****************************************************************

    public class ProjectManagerHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public bool IsProjectManager(string userId, int projectId)
        {
            if (db.ProjectManager.Any(p => p.ProjectId == projectId && p.UserId == userId))
            {
                return true;
            }
            return false;
        }

        public void AddUserToProject(string userId, int projectId)
        {
            // add a 
            if (!this.IsProjectManager(userId, projectId))
            {
                var pu = new ProjectUser { ProjectId = projectId, UserId = userId };
                db.ProjectUsers.Add(pu);
                db.SaveChanges();
            }
        }

        public void RemoveManagerFromProject(string userId, int projectId)
        {
            // Remove a user from the project
            if (this.IsProjectManager(userId, projectId))
            {
                // var pu = new ProjectUser { ProjectId = projectId, UserId = userId };
                // returns a Task object (Because it is aync method)and it contains the result
                var pu = db.ProjectUsers.SingleAsync(p => p.ProjectId == projectId && p.UserId == userId);
                db.ProjectUsers.Remove(pu.Result);
                db.SaveChanges();
            }
        }

        public IList<ApplicationUser> ManagersOnProject(int projectId)
        {
            var userList = new List<ApplicationUser>();
            foreach (var user in db.Users)
            {
                if (this.IsProjectManager(user.Id, projectId))
                {
                    userList.Add(user);
                }
            }
            return userList;
        }

        public IList<ApplicationUser> ManagersOnProject(int projectId, string roleName)
        {
            var userList = new List<ApplicationUser>();
            var rolesHelper = new UserRolesHelper();

            foreach (var user in db.Users)
            {
                if (this.IsProjectManager(user.Id, projectId) && rolesHelper.IsUserInRole(user.Id, roleName))
                {
                    userList.Add(user);
                }
            }
            return userList;
        }

        public IList<ApplicationUser> ManagersNotOnProject(int projectId, string roleName)
        {
            var userList = new List<ApplicationUser>();
            var rolesHelper = new UserRolesHelper();

            foreach (var user in db.Users)
            {
                if (!this.IsProjectManager(user.Id, projectId) && rolesHelper.IsUserInRole(user.Id, roleName))
                {
                    userList.Add(user);
                }
            }
            return userList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">String Id field of Application User</param>
        /// <returns></returns>
        public IList<Project> ListProjectsForManagers(string userId)
        {
            var projectList = new List<Project>();
            foreach (var project in db.Projects)
            {
                if (this.IsProjectManager(userId, project.Id))
                {
                    projectList.Add(project);
                }

            }
            return projectList;
        }

        public IList<Project> ListProjectsNotAssignedToManager(string userId)
        {
            var projectList = new List<Project>();
            foreach (var project in db.Projects)
            {
                if (!this.IsProjectManager(userId, project.Id))
                {
                    projectList.Add(project);
                }

            }
            return projectList;
        }
    }//class UserRoleHelper

    // ********************** Project Developer ****************************************************************

    public class ProjectDeveloperHelper
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public bool IsProjectDeveloper(string userId, int projectId)
        {
            if (db.ProjectManager.Any(p => p.ProjectId == projectId && p.UserId == userId))
            {
                return true;
            }
            return false;
        }

        public void AddDeveloperToProject(string userId, int projectId)
        {
            // add a 
            if (!this.IsProjectDeveloper(userId, projectId))
            {
                var pu = new ProjectUser { ProjectId = projectId, UserId = userId };
                db.ProjectUsers.Add(pu);
                db.SaveChanges();
            }
        }

        public void RemoveDeveloperFromProject(string userId, int projectId)
        {
            // Remove a user from the project
            if (this.IsProjectDeveloper(userId, projectId))
            {
                // var pu = new ProjectUser { ProjectId = projectId, UserId = userId };
                // returns a Task object (Because it is aync method)and it contains the result
                var pu = db.ProjectUsers.SingleAsync(p => p.ProjectId == projectId && p.UserId == userId);
                db.ProjectUsers.Remove(pu.Result);
                db.SaveChanges();
            }
        }

        public IList<ApplicationUser> DeveloperssOnProject(int projectId)
        {
            var userList = new List<ApplicationUser>();
            foreach (var user in db.Users)
            {
                if (this.IsProjectDeveloper(user.Id, projectId))
                {
                    userList.Add(user);
                }
            }
            return userList;
        }

        public IList<ApplicationUser> DevelopersOnProject(int projectId, string roleName)
        {
            var userList = new List<ApplicationUser>();
            var rolesHelper = new UserRolesHelper();

            foreach (var user in db.Users)
            {
                if (this.IsProjectDeveloper(user.Id, projectId) && rolesHelper.IsUserInRole(user.Id, roleName))
                {
                    userList.Add(user);
                }
            }
            return userList;
        }

        public IList<ApplicationUser> DevelopersNotOnProject(int projectId, string roleName)
        {
            var userList = new List<ApplicationUser>();
            var rolesHelper = new UserRolesHelper();

            foreach (var user in db.Users)
            {
                if (!this.IsProjectDeveloper(user.Id, projectId) && rolesHelper.IsUserInRole(user.Id, roleName))
                {
                    userList.Add(user);
                }
            }
            return userList;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId">String Id field of Application User</param>
        /// <returns></returns>
        public IList<Project> ListProjectsForDevelopers(string userId)
        {
            var projectList = new List<Project>();
            foreach (var project in db.Projects)
            {
                if (this.IsProjectDeveloper(userId, project.Id))
                {
                    projectList.Add(project);
                }

            }
            return projectList;
        }

        public IList<Project> ListProjectsNotAssignedToDeveloper(string userId)
        {
            var projectList = new List<Project>();
            foreach (var project in db.Projects)
            {
                if (!this.IsProjectDeveloper(userId, project.Id))
                {
                    projectList.Add(project);
                }

            }
            return projectList;
        }
    }//class UserRoleHelper





}// Namespace

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BugTracker.Models;
using Microsoft.AspNet.Identity;


namespace BugTracker.Controllers
{

    public class ProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private List<ProjectViewModels> projectInfo = new List<ProjectViewModels>();


        // GET: Projects
        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }

        // GET: Projects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // GET: Projects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Projects.Add(project);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(project);
        }

        // GET: Projects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name")] Project project)
        {
            if (ModelState.IsValid)
            {
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // ***************************************************************************************

        [HttpGet]
        public ActionResult ListProjectsAndUsers()
        {
            var userList = new List<ApplicationUser>();
            var helperProject = new UserProjectHelper();
            var helperRole = new UserRolesHelper();

            //var selectListOfRoles = db.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            // same as above but just string array than selected list array
    //        var ListOfUsers = db.Users.OrderBy(r => r.LastName).ToList().Select(rr => rr.LastName.ToString()).ToList();

            var ListOfProjects = db.Projects.OrderBy(p => p.Name).ToList().Select(pp => pp.Name.ToString()).ToList();


            //    var listOfRoles = UserDb.Roles.OrderBy(r => r.Name).ToList().Select(rr => rr.Name.ToString());


            foreach (var user in db.Users)
            {
                var currentUser = new ProjectViewModels();

                // Result converts Task<Ilist> to IList
                var ListOfProjectsForUser = helperProject.ListProjectsAssignedToUser(user.Id);

                currentUser.Projects = ListOfProjectsForUser;
                currentUser.UserId = user.Id;
                currentUser.UserName = user.FirstName + " " + user.LastName;
                currentUser.Role = helperRole.ListUserRoles(user.Id);

                projectInfo.Add(currentUser);

            }
            return View("ListProjectsAndUsers", projectInfo);
        }


        [Authorize(Roles = "Admin, Project Manager")]
        [HttpGet]
        public ActionResult AssignProjectsToUser(string UserId)
        {
            string myUser;
            if (UserId == null || UserId == "")
            {
                myUser = User.Identity.GetUserId();
            }
            else
            {
                myUser = UserId;
            }
            var userList = new List<ApplicationUser>();
            var helperProject = new UserProjectHelper();

            var ListOfProjects = db.Projects.OrderBy(p => p.Name).ToList().Select(pp => pp.Name.ToString()).ToList();

            ProjectToUser projectToUser = new ProjectToUser();

                projectToUser.availableProjects = new MultiSelectList(ListOfProjects);
                projectToUser.assignedProjects = new MultiSelectList(helperProject.ListProjectsAssignedToUser(myUser).Select(n => n.Name));
                projectToUser.notAssignedProjects = new MultiSelectList(helperProject.ListProjectsNotAssignedToUser(myUser).Select(n => n.Name));
                
                projectToUser.UserId = myUser;
 //               projectToUser.UserName = (db.Users.Find(myUser).Email == null) ? "NoEmail" : db.Users.Find(myUser).Email;
                projectToUser.UserName = "None";
           
            return View("AssignProjectsToUser", projectToUser);
        }

        [Authorize(Roles = "Admin, Project Manager")]
        [HttpPost]
        public ActionResult AssignProjectsToUser(string UserId, [Bind(Include = "notAssignedProjects,selectedNotAssignedProjects")] ProjectToUser model,
            string addButton)
        {
            var userList = new List<ApplicationUser>();
            var helperProject = new UserProjectHelper();
            var ListOfProjects = db.Projects.OrderBy(p => p.Name).ToList().Select(pp => pp.Name.ToString()).ToList();


            foreach (var p in model.selectedNotAssignedProjects)
            {
                int pId = db.Projects.FirstOrDefault(n => n.Name == p).Id;
                helperProject.AddUserToProject(UserId, pId);
            }
            return RedirectToAction("ListProjectsAndUsers");
        }

        [HttpPost]
        public ActionResult RemoveProjectsFromUser(string UserId, [Bind(Include = "assignedProjects,selectedAssignedProjects")] ProjectToUser model,
           string removeButton)
        {
            var userList = new List<ApplicationUser>();
            var helperProject = new UserProjectHelper();
            var ListOfProjects = db.Projects.OrderBy(p => p.Name).ToList().Select(pp => pp.Name.ToString()).ToList();


            foreach (var p in model.selectedAssignedProjects)
            {
                int pId = db.Projects.FirstOrDefault(n => n.Name == p).Id;
                helperProject.RemoveUserFromProject(UserId, pId);
            }

            return RedirectToAction("ListProjectsAndUsers");

        }












        //[HttpPost]
        //public ActionResult AssignProjectsToUser(string UserId, [Bind(Include = "availableProjects,selectedProjects")] ProjectToUser model,
        //   string addButton)
        //{
        //    var userList = new List<ApplicationUser>();
        //    var helperProject = new UserProjectHelper();
        //    var ListOfProjects = db.Projects.OrderBy(p => p.Name).ToList().Select(pp => pp.Name.ToString()).ToList();


        //    foreach (var p in model.selectedProjects)
        //    {
        //        int pId = db.Projects.FirstOrDefault(n => n.Name == p).Id;
        //        helperProject.AddUserToProject(UserId, pId);
        //    }
        //    return RedirectToAction("ListProjectsAndUsers");
        //}



    }
}

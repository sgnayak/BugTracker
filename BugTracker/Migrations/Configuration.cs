namespace BugTracker.Migrations
{
    using BugTracker.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BugTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BugTracker.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            //   context.Roles.Add(new IdentityRole { Name = "Admin" });
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);

            }

            if (!context.Roles.Any(r => r.Name == "Project Manager"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Project Manager" };

                manager.Create(role);

            }


            if (!context.Roles.Any(r => r.Name == "Developer"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Developer" };

                manager.Create(role);

            }


            if (!context.Roles.Any(r => r.Name == "Submitter"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Submitter" };

                manager.Create(role);

            }

            // Create a new application user object (AspNetUsers entry)
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            if (!context.Users.Any(u => u.Email == "satyanayak@me.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Satya",
                    LastName = "Nayak",
                    DisplayName = "SN",
                    UserName = "satyanayak@me.com",
                    Email = "satyanayak@me.com"
                },
                "Password-1");
            }

            if (!context.Users.Any(u => u.Email == "avatar@me.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "SatyaAvtar",
                    LastName = "NNNN",
                    DisplayName = "Avtar",
                    UserName = "avatar@me.com",
                    Email = "avatar@me.com"
                },
                "Password-1");
            }

            if (!context.Users.Any(u => u.Email == "blinky@me.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Blink",
                    LastName = "Flash",
                    DisplayName = "Blink",
                    UserName = "blinky@me.com",
                    Email = "blinky@me.com"
                },
                "Password-1");
            }

            if (!context.Users.Any(u => u.Email == "jerry@me.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Jerry",
                    LastName = "Seinfeld",
                    DisplayName = "JEeerryy",
                    UserName = "Jerry@me.com",
                    Email = "Jerry@me.com"
                },
                "Password-1");
            }


            if (!context.Users.Any(u => u.Email == "newman@me.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Fat",
                    LastName = "Newman",
                    DisplayName = "New",
                    UserName = "newman@me.com",
                    Email = "newman@me.com"
                },
                "Password-1");
            }


            if (!context.Users.Any(u => u.Email == "kramer@me.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Cosmo",
                    LastName = "Kramer",
                    DisplayName = "Cosmos",
                    UserName = "kramer@me.com",
                    Email = "kramer@me.com"
                },
                "Password-1");
            }


            if (!context.Users.Any(u => u.Email == "elane@me.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Elane",
                    LastName = "Benese",
                    DisplayName = "EB",
                    UserName = "elane@me.com",
                    Email = "elane@me.com"
                },
                "Password-1");
            }

            var projects = new List<Project>{
                            new Project { Name = "The Big Lebowski" },
                            new Project { Name = "Peterkin" },
                            new Project { Name = "Ferris" },
                            new Project { Name = "Newman" },
                            new Project { Name = "Kramer" }
                        };

            if (!context.Projects.Any(r => r.Name == "The Big Lebowski"))
            {
                projects.ForEach(p => context.Projects.Add(p));
                context.SaveChanges();
            }



            var userId = userManager.FindByEmail("satyanayak@me.com").Id;
            userManager.AddToRole(userId, "Admin");
            userManager.AddToRole(userId, "Developer");

            var userId2 = userManager.FindByEmail("elane@me.com").Id;
            userManager.AddToRole(userId2, "Admin");
            userManager.AddToRole(userId2, "Developer");
            // Commented out Used in Database First design to assign 
            // local user btUser aspNetUserId to aspnetUserId (nvarchar(50))
            //// Create BtUsers entry to match the AspNetUsers entry
            //var bt = new BugTrackerEntities();
            //if(!(bt.BTUsers.Any(u => u.AspNetUserId == userId)))
            //{
            //    var btUser = new BTUser();
            //    btUser.AspNetUserId = userId;
            //    btUser.FirstName = "Satya";
            //    btUser.LastName = "Nayak";
            //    bt.BTUsers.Add(btUser);
            //    bt.SaveChanges();
            //}

            var project = context.Projects.Single(p => p.Name == "The Big Lebowski").Id;
            var project2 = context.Projects.Single(p => p.Name == "Peterkin").Id;
            var project3 = context.Projects.Single(p => p.Name == "Ferris").Id;
            var project4 = context.Projects.Single(p => p.Name == "Newman").Id;
            var project5 = context.Projects.Single(p => p.Name == "Kramer").Id;

            var types = new List<TicketType>
            {
                new TicketType { Name = "Bug" },
                new TicketType { Name = "Featue request" },
                new TicketType { Name = "Improvement" }
            };

            if (!context.TicketTypes.Any(r => r.Name == "Bug"))
            {
                types.ForEach(t => context.TicketTypes.Add(t));
                context.SaveChanges();
            }

            var status = new List<TicketStatus>
            {
                new TicketStatus { Name = "In progress" },
                new TicketStatus { Name = "Not assigned" },
                new TicketStatus { Name = "Closed" }
            };

            if (!context.TicketStatuses.Any(r => r.Name == "Not assigned"))
            {
                status.ForEach(s => context.TicketStatuses.Add(s));
                context.SaveChanges();
            }

            var priority = new List<TicketPriority>
            {
                new TicketPriority { Name = "Low" },
                new TicketPriority { Name = "Medium" },
                new TicketPriority { Name = "High" }
            };

            if (!context.TicketPriorities.Any(p => p.Name == "Not assigned"))
            {
                priority.ForEach(p => context.TicketPriorities.Add(p));
                context.SaveChanges();
            }

            var typeId = context.TicketTypes.Single(p => p.Name == "Bug").Id;
            var statusId = context.TicketStatuses.Single(p => p.Name == "In progress").Id;
            var priorityId = context.TicketPriorities.Single(p => p.Name == "Low");
            var priorityId1 = context.TicketPriorities.Single(p => p.Name == "High");
            var priorityId2 = context.TicketPriorities.Single(p => p.Name == "Medium");
            var priorityId3 = context.TicketPriorities.Single(p => p.Name == "High");

            var tickets = new List<Ticket>
            {
                    new Ticket 
                    {
                        Title = "Search is broken",
                        Description = "The search never returns results",
                        Created = System.DateTimeOffset.Now,
                        ProjectId = project,
                        TicketTypeId = typeId,
                        TicketStatusId = statusId,
                        OwnerUserId = userId,
                        AssignedToUserId = userId,
                        TicketPriority = priorityId,
                        TicketRead = false
                     },  

                    new Ticket {
                        Title = "Can't attach a file to a ticket",
                        Description = "I get an error undefined everytinme",
                        Created = System.DateTimeOffset.Now,
                        ProjectId = project2,
                        TicketTypeId = typeId,
                        TicketStatusId = statusId,
                        OwnerUserId = userId,
                        AssignedToUserId = userId,
                        TicketPriority = priorityId2,
                        TicketRead = false
                     },
                     new Ticket {
                        Title = "Can't reassign a ticket",
                        Description = "The drop down of users doesn't populate",
                        Created = System.DateTimeOffset.Now,
                        ProjectId = project3,
                        TicketTypeId = typeId,
                        TicketStatusId = statusId,
                        OwnerUserId = userId,
                        AssignedToUserId = userId,
                        TicketPriority = priorityId2,
                        TicketRead = false
                     },
                     new Ticket {
                        Title = "Can't change status of a ticket",
                        Description = "Error every time",
                        Created = System.DateTimeOffset.Now,
                        ProjectId = project4,
                        TicketTypeId = typeId,
                        TicketStatusId = statusId,
                        OwnerUserId = userId,
                        AssignedToUserId = userId,
                        TicketPriority = priorityId,
                        TicketRead = false
                      },
                      new Ticket {
                        Title = "Can't create a new project",
                        Description = "Validation error",
                        Created = System.DateTimeOffset.Now,
                        ProjectId = project5,
                        TicketTypeId = typeId,
                        TicketStatusId = statusId,
                        OwnerUserId = userId,
                        AssignedToUserId = userId,
                        TicketPriority = priorityId3,
                        TicketRead = false
                      },
                        new Ticket {
                        Title = "Can't assign users to a ticket",
                        Description = "Drop down list doesn't populate",
                        Created = System.DateTimeOffset.Now,
                        ProjectId = project5,
                        TicketTypeId = typeId,
                        TicketStatusId = statusId,
                        OwnerUserId = userId,
                        AssignedToUserId = userId,
                        TicketPriority = priorityId3,
                        TicketRead = false
                      },
                        new Ticket {
                        Title = "Sorting of rows not working",
                        Description = "When you click on a row nothing happens",
                        Created = System.DateTimeOffset.Now,
                        ProjectId = project4,
                        TicketTypeId = typeId,
                        TicketStatusId = statusId,
                        OwnerUserId = userId,
                        AssignedToUserId = userId,
                        TicketPriority = priorityId2,
                        TicketRead = false
                      },
                        new Ticket {
                        Title = "Create new ticket",
                        Description = "Need a textarea for description",
                        Created = System.DateTimeOffset.Now,
                        ProjectId = project3,
                        TicketTypeId = typeId,
                        TicketStatusId = statusId,
                        OwnerUserId = userId,
                        AssignedToUserId = userId,
                        TicketPriority = priorityId1,
                        TicketRead = false
                       },
                        new Ticket {
                        Title = "Timestamps are editable",
                        Description = "Really? How convenient",
                        Created = System.DateTimeOffset.Now,
                        ProjectId = project2,
                        TicketTypeId = typeId,
                        TicketStatusId = statusId,
                        OwnerUserId = userId,
                        AssignedToUserId = userId,
                        TicketPriority = priorityId2,
                        TicketRead = false
                       },
                    new Ticket {
                        Title = "Save after editing broken",
                        Description = "More validation errors",
                        Created = System.DateTimeOffset.Now,
                        ProjectId = project,
                        TicketTypeId = typeId,
                        TicketStatusId = statusId,
                        OwnerUserId = userId,
                        AssignedToUserId = userId,
                        TicketPriority = priorityId,
                        TicketRead = false
                       }
            };

            if (!context.Tickets.Any(r => r.ProjectId == project))
            {
                tickets.ForEach(t => context.Tickets.Add(t));
                context.SaveChanges();
            }


        }

    }
}

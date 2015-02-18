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
                    DisplayName = "Satya Nayak",
                    UserName = "satyanayak@me.com",
                    Email = "satyanayak@me.com"
                },
                "Password-1");
            }
            var userIdSatya = userManager.FindByEmail("satyanayak@me.com").Id;
            userManager.AddToRole(userIdSatya, "Admin");
            userManager.AddToRole(userIdSatya, "Developer");
            userManager.AddToRole(userIdSatya, "Project Manager");



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
            var userIdAvatar = userManager.FindByEmail("avatar@me.com").Id;
            userManager.AddToRole(userIdAvatar, "Developer");
            //Users for Project Apollow 13 

            if (!context.Users.Any(u => u.Email == "JimLovell@apollo13.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Jim",
                    LastName = "Lovell",
                    DisplayName = "Jim Lovell",
                    UserName = "JimLovell@apollo13.com",
                    Email =    "JimLovell@apollo13.com"
                },
                "Password-1");
            }
            var userIdJim = userManager.FindByEmail("JimLovell@apollo13.com").Id;
            userManager.AddToRole(userIdJim, "Developer");

            if (!context.Users.Any(u => u.Email == "JackSwigert@apollo13.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Jack",
                    LastName = "Swigert",
                    DisplayName = "Jack Swigert",
                    UserName = "JackSwigert@apollo13.com",
                    Email =    "JackSwigert@apollo13.com"
                },
                "Password-1");
            }
            var userIdJack = userManager.FindByEmail("JackSwigert@apollo13.com").Id;
            userManager.AddToRole(userIdJack, "Submitter");

            if (!context.Users.Any(u => u.Email == "FredHaise@apollo13.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Fred",
                    LastName = "Haise",
                    DisplayName = "Fred Haise",
                    UserName = "FredHaise@apollo13.com",
                    Email =    "FredHaise@apollo13.com"
                },
                "Password-1");
            }
            var userIdFred = userManager.FindByEmail("FredHaise@apollo13.com").Id;
            userManager.AddToRole(userIdFred, "Developer");

            if (!context.Users.Any(u => u.Email == "GeneKranz@apollo13.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Gene",
                    LastName = "Krantz",
                    DisplayName = "Gene Krantz",
                    UserName = "GeneKranz@apollo13.com",
                    Email =    "GeneKranz@apollo13.com"
                },
                "Password-1");
            }
            var userIdKranz = userManager.FindByEmail("GeneKranz@apollo13.com").Id;
            userManager.AddToRole(userIdKranz, "Project Manager");


            // ********   Users for Project Seinfeld

            if (!context.Users.Any(u => u.Email == "JerrySeinfeld@seinfeld.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Jerry",
                    LastName = "Seinfeld",
                    DisplayName = "Jerry Seinfeld",
                    UserName = "JerrySeinfeld@seinfeld.com",
                    Email =    "JerrySeinfeld@seinfeld.com"
                },
                "Password-1");
            }
            var userIdJerry = userManager.FindByEmail("JerrySeinfeld@seinfeld.com").Id;
            userManager.AddToRole(userIdJerry, "Project Manager");

            if (!context.Users.Any(u => u.Email == "MichaelRichards@seinfeld.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Michael",
                    LastName = "Richards",
                    DisplayName = "Michael Richards",
                    UserName = "MichaelRichards@seinfeld.com",
                    Email =    "MichaelRichards@seinfeld.com"
                },
                "Password-1");
            }
            var userIdKramer = userManager.FindByEmail("MichaelRichards@seinfeld.com").Id;
            userManager.AddToRole(userIdKramer, "Admin");

            if (!context.Users.Any(u => u.Email == "JasonAlexander@seinfeld.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Jason",
                    LastName = "Alexander",
                    DisplayName = "Jason Alexander",
                    UserName = "JasonAlexander@seinfeld.com",
                    Email =    "JasonAlexander@seinfeld.com"
                },
                "Password-1");
            }
            var userIdGeorge = userManager.FindByEmail("JasonAlexander@seinfeld.com").Id;
            userManager.AddToRole(userIdGeorge, "Developer");

            if (!context.Users.Any(u => u.Email == "JuliaDreyfus@seinfeld.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Julia",
                    LastName = "Dreyfus",
                    DisplayName = "Julia Dreyfus",
                    UserName = "JuliaDreyfus@seinfeld.com",
                    Email =    "JuliaDreyfus@seinfeld.com"
                },
                "Password-1");
            }
            var userIdElane = userManager.FindByEmail("JuliaDreyfus@seinfeld.com").Id;
            userManager.AddToRole(userIdElane, "Project Manager");


            if (!context.Users.Any(u => u.Email == "LaryDavid@seinfeld.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Lary",
                    LastName = "David",
                    DisplayName = "Lary David",
                    UserName = "LaryDavid@seinfeld.com",
                    Email = "LaryDavid@seinfeld.com"
                },
                "Password-1");
            }
            var userIdLarry = userManager.FindByEmail("LaryDavid@seinfeld.com").Id;
            userManager.AddToRole(userIdLarry, "Admin");


            if (!context.Users.Any(u => u.Email == "WayneKnight@seinfeld.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Wayne",
                    LastName = "Knight",
                    DisplayName = "Newman",
                    UserName = "WayneKnight@seinfeld.com",
                    Email =    "WayneKnight@seinfeld.com"
                },
                "Password-1");
            }
            var userIdNewman = userManager.FindByEmail("WayneKnight@seinfeld.com").Id;
            userManager.AddToRole(userIdNewman, "Submitter");


            // *********  Project Skunk Works

            if (!context.Users.Any(u => u.Email == "BenRich@lockheed.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Ben",
                    LastName = "Rich",
                    DisplayName = "Ben Rich",
                    UserName = "BenRich@lockheed.com",
                    Email = "BenRich@lockheed.com"
                },
                "Password-1");
            }
            var userIdBen = userManager.FindByEmail("BenRich@lockheed.com").Id;
            userManager.AddToRole(userIdBen, "Admin");


            if (!context.Users.Any(u => u.Email == "KellyJohnson@lockheed.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Kelly",
                    LastName = "Johnson",
                    DisplayName = "Kelly Johnson",
                    UserName = "KellyJohnson@lockheed.com",
                    Email = "KellyJohnson@lockheed.com"
                },
                "Password-1");
            }
            var userIdKelly = userManager.FindByEmail("KellyJohnson@lockheed.com").Id;
            userManager.AddToRole(userIdKelly, "Admin");

            if (!context.Users.Any(u => u.Email == "JosephWare@lockheed.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Joseph",
                    LastName = "Ware",
                    DisplayName = "Joseph Ware",
                    UserName = "JosephWare@lockheed.com",
                    Email = "JosephWare@lockheed.com"
                },
                "Password-1");
            }
            var userIdJoseph = userManager.FindByEmail("JosephWare@lockheed.com").Id;
            userManager.AddToRole(userIdJoseph, "Submitter");

            if (!context.Users.Any(u => u.Email == "JamesSullivan@lockheed.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "James",
                    LastName = "Sullivan",
                    DisplayName = "James Sullivan",
                    UserName = "JamesSullivan@lockheed.com",
                    Email = "JamesSullivan@lockheed.com"
                },
                "Password-1");
            }
            var userIdJames = userManager.FindByEmail("JamesSullivan@lockheed.com").Id;
            userManager.AddToRole(userIdJames, "Submitter");


            // project Orion
            if (!context.Users.Any(u => u.Email == "CleonLacefield@nasa.gov"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Cleon",
                    LastName = "Lacefiled",
                    DisplayName = "Cleon Lacefiled",
                    UserName = "CleonLacefield@nasa.gov",
                    Email = "CleonLacefield@nasa.gov"
                },
                "Password-1");
            }
            var userIdCleon = userManager.FindByEmail("CleonLacefield@nasa.gov").Id;
            userManager.AddToRole(userIdCleon, "Admin");

            if (!context.Users.Any(u => u.Email == "LarryPrice@nasa.gov"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Larry",
                    LastName = "Price",
                    DisplayName = "Larry Price",
                    UserName = "LarryPrice@nasa.gov",
                    Email = "LarryPrice@nasa.gov"
                },
                "Password-1");
            }
            var userIdPrice = userManager.FindByEmail("LarryPrice@nasa.gov").Id;
            userManager.AddToRole(userIdPrice, "Project Manager");

            if (!context.Users.Any(u => u.Email == "BillJohns@nasa.gov"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Bill",
                    LastName = "Johns",
                    DisplayName = "Bill Johns",
                    UserName = "BillJohns@nasa.gov",
                    Email = "BillJohns@nasa.gov"
                },
                "Password-1");
            }
            var userIdBill = userManager.FindByEmail("BillJohns@nasa.gov").Id;
            userManager.AddToRole(userIdBill, "Developer");

            if (!context.Users.Any(u => u.Email == "GinaCuret@nasa.gov"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Gina",
                    LastName = "Curet",
                    DisplayName = "Gina Curet",
                    UserName = "GinaCuret@nasa.gov",
                    Email = "GinaCuret@nasa.gov"
                },
                "Password-1");
            }
            var userIdGina = userManager.FindByEmail("GinaCuret@nasa.gov").Id;
            userManager.AddToRole(userIdGina, "Project Manager");

            // *************Project Tesla *********

            if (!context.Users.Any(u => u.Email == "ElonMusk@teslamotors.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Elon",
                    LastName = "Musk",
                    DisplayName = "Elon Musk",
                    UserName = "ElonMusk@teslamotors.com",
                    Email = "ElonMusk@teslamotors.com"
                },
                "Password-1");
            }
            var userIdElon = userManager.FindByEmail("ElonMusk@teslamotors.com").Id;
            userManager.AddToRole(userIdElon, "Admin");

            if (!context.Users.Any(u => u.Email == "BradBuss@teslamotors.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Brad",
                    LastName = "Buss",
                    DisplayName = "Brad Buss",
                    UserName = "BradBuss@teslamotors.com",
                    Email = "BradBuss@teslamotors.com"
                },
                "Password-1");
            }
            var userIdBrad = userManager.FindByEmail("BradBuss@teslamotors.com").Id;
            userManager.AddToRole(userIdBrad, "Project Manager");

            if (!context.Users.Any(u => u.Email == "IraEhrenpreis@teslamotors.com"))
            {
                userManager.Create(new ApplicationUser
                {
                    FirstName = "Ira",
                    LastName = "Ehrenpreis",
                    DisplayName = "Ira Ehrenpreis",
                    UserName = "IraEhrenpreis@teslamotors.com",
                    Email = "IraEhrenpreis@teslamotors.com"
                },
                "Password-1");
            }
            var userIdIra = userManager.FindByEmail("IraEhrenpreis@teslamotors.com").Id;
            userManager.AddToRole(userIdIra, "Developer");

            var projects = new List<Project>{
                            new Project { Name = "Apollo 13" },
                            new Project { Name = "Seinfeld" },
                            new Project { Name = "Skunk Works" },
                            new Project { Name = "Orion" },
                            new Project { Name = "Tesla" }
                        };

            if (!context.Projects.Any(r => r.Name == "Apollo 13"))
            {
                projects.ForEach(p => context.Projects.Add(p));
                context.SaveChanges();
            }

            var projectApollo = context.Projects.Single(p => p.Name == "Apollo 13").Id;
            var projectSeinfeld = context.Projects.Single(p => p.Name == "Seinfeld").Id;
            var projectSkunk = context.Projects.Single(p => p.Name == "Skunk Works").Id;
            var projectOrion = context.Projects.Single(p => p.Name == "Orion").Id;
            var projectTesla = context.Projects.Single(p => p.Name == "Tesla").Id;


            //var userIdSatya = userManager.FindByEmail("satyanayak@me.com").Id;
            //userManager.AddToRole(userIdSatya, "Admin");
            //userManager.AddToRole(userIdSatya, "Developer");


            var types = new List<TicketType>
            {
                new TicketType { Name = "Bug" },
                new TicketType { Name = "Unknown" },
                new TicketType { Name = "Improvement" }
            };

            if (!context.TicketTypes.Any(r => r.Name == "Bug"))
            {
                types.ForEach(t => context.TicketTypes.Add(t));
                context.SaveChanges();
            }

            var typeBug = context.TicketTypes.Single(p => p.Name == "Bug").Id;
            var typeUnknown = context.TicketTypes.Single(p => p.Name == "Unknown").Id;
            var typeImprovement = context.TicketTypes.Single(p => p.Name == "Improvement").Id;

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

            var statusInProgress = context.TicketStatuses.Single(s => s.Name == "In Progress").Id;
            var statusNotAssigned = context.TicketStatuses.Single(s => s.Name == "Not Assigned").Id;
            var statusClosed = context.TicketStatuses.Single(s => s.Name == "Closed").Id;


            var priority = new List<TicketPriority>
            {
                new TicketPriority { Name = "Low" },
                new TicketPriority { Name = "Medium" },
                new TicketPriority { Name = "High" }
            };

            if (!context.TicketPriorities.Any(p => p.Name == "Low"))
            {
                priority.ForEach(p => context.TicketPriorities.Add(p));
                context.SaveChanges();
            }


            var priorityLow = context.TicketPriorities.Single(p => p.Name == "Low");
            var priorityHigh = context.TicketPriorities.Single(p => p.Name == "High");
            var priorityMedium = context.TicketPriorities.Single(p => p.Name == "Medium");

            var ticketsApollo = new List<Ticket>
            {
                    new Ticket 
                    {
                        Title = "Houston We Have a Problem",
                        Description = "After completing the broadcast, flight control sent another message, 13, we got one more item for you when you get a chance. We'd like you to err, stir up your cryo tanks. In addition err, have a shaft and trunnion, for a look at the comet Bennett if you need it.Astronaut Jack Swigert replied, OK, stand by",
                        Created = System.DateTimeOffset.Now,
                        ProjectId = projectApollo,
                        TicketTypeId = typeBug,
                        TicketStatusId = statusInProgress,
                        OwnerUserId = userIdJim,
                        AssignedToUserId = userIdKranz,
                        TicketPriority = priorityHigh,
                        TicketRead = false
                     },  

                    new Ticket {
                        Title = "Can't attach a file to a ticket",
                        Description = "It will be nice to have an Oxigen tank that works. Defect was found on the oxygen tank",
                        Created = System.DateTimeOffset.UtcNow,
                        ProjectId = projectApollo,
                        TicketTypeId = typeImprovement,
                        TicketStatusId = statusInProgress,
                        OwnerUserId = userIdJack,
                        AssignedToUserId = userIdKranz,
                        TicketPriority = priorityHigh,
                        TicketRead = false
                     }

            };

            if (!context.Tickets.Any(r => r.ProjectId == projectApollo))
            {
                ticketsApollo.ForEach(t => context.Tickets.Add(t));
                context.SaveChanges();
            }


            var ticketsSeinfeld = new List<Ticket>
            {
                    new Ticket 
                    {
                        Title = "Unfunny",
                        Description = "Seinfeld began as a 23-minute pilot named The Seinfeld Chronicles. Created by stand-up comedian Jerry Seinfeld and writer Larry David, developed by NBC executive Rick Ludwin, and produced by Castle Rock Entertainment, it was a mix of Seinfeld's stand-up comedy routines and idiosyncratic, conversational scenes focusing on mundane aspects of everyday life such as laundry, the buttoning of the top button on one's shirt and the attempt by men to properly interpret the intent of women spending the night in Seinfeld's apartment.[7]The pilot was filmed at Stage 8 of Desilu Cahuenga studios, the same studio where The Dick Van Dyke Show was filmed (this was seen by the crew as a good omen),[8] and was recorded at Ren-Mar Studios in Hollywood.[9] The pilot was first screened to a group of two dozen NBC executives in Burbank, California in early 1989. Although it did not yield the explosion of laughter garnered by the pilots for the decades previous NBC successes like The Cosby Show and The Golden Girls, it drew a positive response from the assembled executives, such as Warren Littlefield, then the second-in-command for NBCs entertainment division, who relates, There was a sense this was something different. The room embraced the humor and the attitude. Littlefields boss, Brandon Tartikoff, by contrast, was not convinced that the show would work. A Jewish man from New York himself, Tartikoff characterized it as Too New York, too Jewish. Test audiences were even more harsh. NBCs practice at the time was to recruit 400 households by phone to ask them to evaluate pilots it aired on an unused channel on its cable system. An NBC research department memo summarized the pilots performance among the respondents as Weak, which Littlefield called a dagger to the heart.[7] Comments included, You cant get too excited about two guys going to the laundromat; Jerrys loser friend George is not a forceful character; Jerry needs a stronger supporting cast; and Why are they interrupting the stand-up for these stupid stories?",
                        Created = System.DateTimeOffset.Now,
                        ProjectId = projectSeinfeld,
                        TicketTypeId = typeImprovement,
                        TicketStatusId = statusClosed,
                        OwnerUserId = userIdJerry,
                        AssignedToUserId = userIdJerry,
                        TicketPriority = priorityLow,
                        TicketRead = false
                     },  

                    new Ticket {
                        Title = "Bro Manzier",
                        Description = "Manzier  Fans of Seinfeld will remember the episode in which Kramer and Frank Costanza invented a men's bra. But the project froze because they couldn't agree on whether to call it a bro or a manzier",
                        Created = System.DateTimeOffset.UtcNow,
                        ProjectId = projectSeinfeld,
                        TicketTypeId = typeImprovement,
                        TicketStatusId = statusInProgress,
                        OwnerUserId = userIdElane,
                        AssignedToUserId = userIdJerry,
                        TicketPriority = priorityHigh,
                        TicketRead = false
                     }

            };

            if (!context.Tickets.Any(r => r.ProjectId == projectSeinfeld))
            {
                ticketsSeinfeld.ForEach(t => context.Tickets.Add(t));
                context.SaveChanges();
            }


            var ticketsSkunk = new List<Ticket>
            {
                    new Ticket 
                    {
                        Title = "U2",
                        Description = "After World War II, the U.S. military desired better strategic reconnaissance to help determine Soviet capabilities and intentions. Into the 1950s, the best intelligence the American government had on the interior of the Soviet Union were German Luftwaffe photographs taken during the war of territory west of the Ural Mountains, so overflights to take aerial photographs of the Soviet Union began. After 1950, Soviet air defenses aggressively attacked all aircraft near its borders—sometimes even those over Japanese airspace—and the existing reconnaissance aircraft, primarily bombers converted for reconnaissance duty such as the Boeing RB-47, were vulnerable to anti-aircraft artillery, missiles, and fighters. Richard Leghorn of the United States Air Force suggested that an aircraft that could fly at 60,000 feet (18,300 m) should be safe from the MiG-17, the Soviet Union's best interceptor, which could barely reach 45,000 feet (13,700 m). He and others believed that Soviet radar, which used American equipment provided during the war, could not track aircraft above 65,000 feet.",
                        Created = System.DateTimeOffset.Now,
                        ProjectId = projectSkunk,
                        TicketTypeId = typeImprovement,
                        TicketStatusId = statusNotAssigned,
                        OwnerUserId = userIdKelly,
                        AssignedToUserId = userIdJoseph,
                        TicketPriority = priorityLow,
                        TicketRead = false
                     },  

                    new Ticket {
                        Title = "U2 over USSR",
                        Description = "In July 1957, U.S. President Dwight D. Eisenhower requested permission from Pakistan's Prime Minister Huseyn Suhrawardy for the U.S. to establish a secret intelligence facility in Pakistan and for the U-2 spyplane to fly from Pakistan. The U-2 flew at altitudes that could not be reached by Soviet fighter jets of the era; it was believed to be beyond the reach of Soviet missiles as well. A facility established in Badaber (Peshawar Air Station), 10 miles (16 km) from Peshawar, was a cover for a major communications intercept operation run by the United States National Security Agency (NSA). Badaber was an excellent location because of its proximity to Soviet central Asia. This enabled the monitoring of missile test sites, key infrastructure and communications. The U-2 spy-in-the-sky was allowed to use the Pakistan Air Force portion of Peshawar Airport to gain vital photo intelligence in an era before satellite observation",
                        Created = System.DateTimeOffset.UtcNow,
                        ProjectId = projectSkunk,
                        TicketTypeId = typeUnknown,
                        TicketStatusId = statusInProgress,
                        OwnerUserId = userIdJoseph,
                        AssignedToUserId = userIdBen,
                        TicketPriority = priorityHigh,
                        TicketRead = false
                     },
                     
                    new Ticket {
                        Title = "SR 71 fastest plane ever built NickName BlackBird",
                        Description = "The SR-71 was designed for flight at over Mach 3 with a flight crew of two in tandem cockpits, with the pilot in the forward cockpit and the Reconnaissance Systems Officer (RSO) monitoring the surveillance systems and equipment from the rear cockpit.[20] The SR-71 was designed to minimize its radar cross-section, an early attempt at stealth design.[21] Finished aircraft were painted a dark blue, almost black, to increase the emission of internal heat and to act as camouflage against the night sky. The dark color led to the aircraft's call sign Blackbird.While the SR-71 carried electronic countermeasures to evade interception efforts, its greatest protection was its high speed and cruising altitude that made it almost invulnerable to the weapons of its day. Merely accelerating would typically be enough to evade a surface-to-air missile,[2] and the plane was faster than the Soviet Unions principal interceptor, the MiG-25.[22] During its service life, no SR-71 was shot down.",
                        Created = System.DateTimeOffset.UtcNow,
                        ProjectId = projectSkunk,
                        TicketTypeId = typeUnknown,
                        TicketStatusId = statusInProgress,
                        OwnerUserId = userIdKelly,
                        AssignedToUserId = userIdJames,
                        TicketPriority = priorityHigh,
                        TicketRead = false
                     },

                    new Ticket {
                        Title = "SR 71 Reactivation request",
                        Description = "Due to unease over political situations in the Middle East and North Korea, the U.S. Congress re-examined the SR-71 beginning in 1993.[83] Rear Admiral Thomas F. Hall addressed the question of why the SR-71 was retired, saying it was under the belief that, given the time delay associated with mounting a mission, conducting a reconnaissance, retrieving the data, processing it, and getting it out to a field commander, that you had a problem in timelines that was not going to meet the tactical requirements on the modern battlefield. And the determination was that if one could take advantage of technology and develop a system that could get that data back real time... that would be able to meet the unique requirements of the tactical commander. Hall stated they were looking at alternative means of doing [the job of the SR-71].",
                        Created = System.DateTimeOffset.UtcNow,
                        ProjectId = projectSkunk,
                        TicketTypeId = typeUnknown,
                        TicketStatusId = statusInProgress,
                        OwnerUserId = userIdJames,
                        AssignedToUserId = userIdSatya,
                        TicketPriority = priorityHigh,
                        TicketRead = false
                     }

            };



            if (!context.Tickets.Any(r => r.ProjectId == projectSkunk))
            {
                ticketsSkunk.ForEach(t => context.Tickets.Add(t));
                context.SaveChanges();
            }



            var ticketsOrion = new List<Ticket>
            {
                    new Ticket 
                    {
                        Title = "New Human Module for Future Mission To Mars",
                        Description = "The Orion MPCV takes basic design elements from the Apollo Command Module that took astronauts to the moon, but its technology and capability are more advanced. It is designed to support long-duration deep space missions, with up to 21 days active crew time plus 6 months quiescent.[33] During the quiescent period crew life support would be provided by another module such as a Deep Space Habitat. The spacecraft's life support, propulsion, thermal protection and avionics systems are designed to be upgradeable as new technologies become available. The MPCV spacecraft includes both crew and service modules, and a spacecraft adaptor. The MPCVs crew module is larger than Apollos and can support more crew members for short or long-duration missions. The service module fuels and propels the spacecraft as well as storing oxygen and water for astronauts. The service modules structure is also being designed to provide locations to mount scientific experiments and cargo.",
                        Created = System.DateTimeOffset.UtcNow,
                        ProjectId = projectSeinfeld,
                        TicketTypeId = typeBug,
                        TicketStatusId = statusInProgress,
                        OwnerUserId = userIdBill,
                        AssignedToUserId = userIdSatya,
                        TicketPriority = priorityMedium,
                        TicketRead = false
                     },  

                    new Ticket {
                        Title = "Project Mars",
                        Description = "The exploration of Mars has taken place over hundreds of years, beginning in earnest with the invention and development of the telescope during the 1600s. Increasingly detailed views of the planet from Earth inspired speculation about its environment and possible life – even intelligent civilizations – that might be found there. Probes sent from Earth beginning in the late 20th century have yielded a dramatic increase in knowledge about the Martian system, focused primarily on understanding its geology and habitability potential. Engineering interplanetary journeys is very complicated, so the exploration of Mars has experienced a high failure rate, especially in earlier attempts. Roughly two-thirds of all spacecraft destined for Mars failed before completing their missions, and there are some that failed before their observations could begin. However, missions have also met with unexpected levels of success, such as the twin Mars Exploration Rovers operating for years beyond their original mission specifications.",
                        Created = System.DateTimeOffset.Now,
                        ProjectId = projectSeinfeld,
                        TicketTypeId = typeUnknown,
                        TicketStatusId = statusInProgress,
                        OwnerUserId = userIdGina,
                        AssignedToUserId = userIdSatya,
                        TicketPriority = priorityHigh,
                        TicketRead = false
                     }

            };



            if (!context.Tickets.Any(r => r.ProjectId == projectOrion))
            {
                ticketsOrion.ForEach(t => context.Tickets.Add(t));
                context.SaveChanges();
            }

            int ticketId = context.Tickets.FirstOrDefault(u => u.Title == "Project Mars").Id;
            var ticketsHistory = new List<TicketHistory>
            {
                new TicketHistory
                {
                    TicketId = ticketId,
                    ProjectName = "Orion",
                    OldProjectName = "Apollo 13",
                    TicketTypeName = "In Progress",
                    OldTicketTypeName = "Bug"
                },

                new TicketHistory
                {
                    TicketId = ticketId,
                    TicketPriorityName = "High",
                    OldTicketPriorityName = "Low",
                    TicketTypeName = "In Progress",
                    OldTicketTypeName = "Bug"
                }
            };

            if (!context.Tickets.Any(u => u.Title == "Project Mars"))
            {
                ticketsHistory.ForEach(h => context.TicketHistories.Add(h));
                context.SaveChanges();
            }
        }

    }
}

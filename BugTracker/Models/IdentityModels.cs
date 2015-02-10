using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System;

namespace BugTracker.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        // FirstName, LastName Display Names were added to ApplicationUser class
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public Nullable<System.DateTimeOffset> TimeLastLogOn { get; set; }
        public Nullable<System.DateTimeOffset> TimeLastLogOff { get; set; }

        // This was required in old version new Community Version This is not required.
        // As it picks up from DbSet
        // Also added This class(User) can have many Tickets
        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Ticket> Tickets { get; set; }
        public virtual ICollection<TicketAttachment> TicketAttachments { get; set; }
        public virtual ICollection<TicketComment> TicketComments { get; set; }
        public virtual ICollection<TicketHistory> TicketHistories { get; set; }
        public virtual ICollection<TicketNotification> TicketNotifications { get; set; }

        //public ApplicationUser()
        //{
        //    this.Tickets = new HashSet<Ticket>();
        //    this.TicketAttachments = new HashSet<TicketAttachment>();
        //    this.TicketComments = new HashSet<TicketComment>();
        //    this.TicketHistories = new HashSet<TicketHistory>();
        //    this.TicketNotifications = new HashSet<TicketNotification>();

        //}

        //// Also added This class(User) can have many Tickets Attachments
        //public virtual ICollection<TicketAttachment> TicketAttachments { get; set; }
        //public ApplicationUser()
        //{
        //    this.TicketAttachments = new HashSet<TicketAttachment>();
        //}

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        // Creates DataBase Tables for following as part of ApplicationDbContext
        public virtual DbSet<Project> Projects { get; set; }
        //public virtual DbSet<ProjectUser> ProjectUsers { get; set; }
        //public virtual DbSet<ProjectManager> ProjectManager { get; set; }
        //public virtual DbSet<ProjectDevelopers> ProjectDevelopers { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }
        public virtual DbSet<TicketAttachment> TicketAttachments { get; set; }
        public virtual DbSet<TicketComment> TicketComments { get; set; }
        public virtual DbSet<TicketHistory> TicketHistories { get; set; }
        public virtual DbSet<TicketNotification> TicketNotifications { get; set; }
        public virtual DbSet<TicketPriority> TicketPriorities { get; set; }
        public virtual DbSet<TicketStatus> TicketStatuses { get; set; }
        public virtual DbSet<TicketType> TicketTypes { get; set; }

    }
}
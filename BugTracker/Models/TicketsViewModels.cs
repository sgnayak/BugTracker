namespace BugTracker.Models
{
    using System.Data.Entity;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public partial class TicketViewModels
    {
        public Ticket ticketView { get; set; }
        public TicketAttachment ticketAttachmentView { get; set; }
    }

}
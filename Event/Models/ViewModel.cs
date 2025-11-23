using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace Event.Models
{
    //==========================================================//
    //============Organizer Upload Detail of Event==============//
    //==========================================================//
    public class OcreateVM
    {
        [Key]
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public string description { get; set; }
        public int TotalTicket { get; set; }
        public DateTime StartT { get; set; }
        public DateTime EndT { get; set; }
        public DateTime CreatedT { get; set; }
        public int OrganizerId { get; set; }
        public int VenueId { get; set; }
        public List<SelectListItem> VenueList { get; set; }
    }
}

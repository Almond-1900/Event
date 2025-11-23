using System.ComponentModel.DataAnnotations;

namespace Event.Models
{
 public class Users
    {
        [Key]
        public int UserId { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
    }

    public class Event
    {
        [Key]
        public int EventId { get; set; }
        public string EventTitle { get; set; }
        public string description { get; set; }
        public DateTime StartT { get; set; }
        public DateTime EndT { get; set; }
        public DateTime CreatedT { get; set; }
        public int OrganizerId { get; set; }
         public int VenueId { get; set; }
    }

    public class Venue
    {
        [Key]
        public int VenueId { get; set; }
        public string VenueName { get; set; }
        public string description { get; set; }
    }

    public class Orders
    {
        [Key]
        public int OrdersId { get; set; }
        public string Status { get; set; }
        public DateTime OrderAt { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }

    }

    public class Ticket
    {
        [Key]
        public int TicketId { get; set; }
        public int Quantity { get; set; }
        public int OrdersId { get; set; }
    }

    public class Payment
    {
        [Key]
        public int PaymentId { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime PaidAt { get; set; }
        public int OrdersId { get; set; }
    }








}

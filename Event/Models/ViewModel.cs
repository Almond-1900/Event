using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
namespace Event.Models
{
    //==========================================================//
    //============Organizer Upload Detail of Event==============//
    //==========================================================//
    public class EventVM
    {
        [Key]
        public int EventId { get; set; }
        public string  EventTitle { get; set; }
        public string  description { get; set; }
        public DateTime StartT { get; set; }
        public DateTime EndT { get; set; }
        public DateTime CreatedT { get; set; }
        public int OrganizerId { get; set; }
        public int VenueId { get; set; }
        public List<SelectListItem> VenueList { get; set; }
    }

    //==========================================================//
    //=====================Login ViewModel======================//
    //==========================================================//
    public class LoginVM
    {
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100, MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }

    public class RegisterVM
    {
        [StringLength(100, MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(100, MinimumLength = 5)]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string Confirm { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(100)]
        [Remote("CheckEmail", "Account", ErrorMessage = "Duplicated {0}.")]
        [EmailAddress]
        public string Email { get; set; }

        [StringLength(100)]
        public string Role { get; set; }
    }

    public class UpdatePasswordVM
    {
        [StringLength(100, MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "Current Password")]
        public string Current { get; set; }

        [StringLength(100, MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Display(Name = "New Password")]
        public string New { get; set; }

        [StringLength(100, MinimumLength = 5)]
        [Compare("New")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Password")]
        public string Confirm { get; set; }
    }

    public class UpdateProfileVM
    {
        public int? UserId { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

    }

    public class ResetPasswordVM
    {
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }
    }

}

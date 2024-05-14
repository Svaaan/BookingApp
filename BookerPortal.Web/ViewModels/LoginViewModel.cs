using Booking.Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace BookerPortal.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide User Name")]
        public string? Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide Password")]
        public string? Password { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string? Name { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string? SurName { get; set; }
    }
}

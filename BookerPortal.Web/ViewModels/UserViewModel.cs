using System.ComponentModel.DataAnnotations;

namespace BookerPortal.Web.ViewModels
{
    public class UserViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide Name")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide LastName")]
        public string LastName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide Email")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide Password")]
        public string Password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide CompanyId")]
        public int CompanyId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide Role")]
        public string Role { get; set; }
    }
}

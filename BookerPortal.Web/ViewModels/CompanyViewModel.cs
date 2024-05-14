using System.ComponentModel.DataAnnotations;

namespace BookerPortal.Web.ViewModels
{
    public class CompanyViewModel
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide CompanyName")]
        public string? CompanyName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please provide Email adress")]
        public string? Email { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string? Adress { get; set; }

        [Required(AllowEmptyStrings = true)]
        public string? Country { get; set; }
    }
}

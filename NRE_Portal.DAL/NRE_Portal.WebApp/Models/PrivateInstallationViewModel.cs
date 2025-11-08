using System.ComponentModel.DataAnnotations;

namespace NRE_Portal.WebApp.Models
{
    public class PrivateInstallationViewModel
    {
        [Required(ErrorMessage = "Street is required")]
        [Display(Name = "Street")]
        public string Street { get; set; } = string.Empty;

        [Required(ErrorMessage = "Street number is required")]
        [Display(Name = "No")]
        public string StreetNo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Postcode is required")]
        [Display(Name = "Postcode")]
        public string Postcode { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required")]
        [Display(Name = "City")]
        public string City { get; set; } = string.Empty;
    }
}
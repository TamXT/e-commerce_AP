using System.ComponentModel.DataAnnotations;

namespace e_commerce_AP.Models
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

}

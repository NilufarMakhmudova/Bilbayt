using System.ComponentModel.DataAnnotations;

namespace Bilbayt.WebClient.Models
{
    public class CreateUserModel
    {
        /// <summary>
        ///     First name
        /// </summary>
        [Required]
        [Display(Name = "First name")]
        public string FirstName { get; set; }
        /// <summary>
        ///     First name
        /// </summary>
        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }
        /// <summary>
        ///     Email
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Username { get; set; }
        /// <summary>
        ///     Password
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        /// <summary>
        ///     Confirmed password
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmedPassword { get; set; }
    }
}

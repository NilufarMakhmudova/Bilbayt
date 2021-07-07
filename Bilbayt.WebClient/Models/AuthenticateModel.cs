using System.ComponentModel.DataAnnotations;

namespace Bilbayt.WebClient.Models
{
    public class AuthenticateModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Bilbayt.Models.AppUser
{
    /// <summary>
    ///     App User Api Model
    /// </summary>
    public class AppUserModel
    {
        /// <summary>
        ///     AppUser Id
        /// </summary>
        [Required]
        public string Id { get; set; }
        /// <summary>
        ///     First name of the user
        /// </summary>
        [Required]
        public string FirstName { get; set; }
        /// <summary>
        ///     Last name of the user
        /// </summary>
        [Required]
        public string LastName { get; set; }
       
        /// <summary>
        ///     Full name
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";
    }
}

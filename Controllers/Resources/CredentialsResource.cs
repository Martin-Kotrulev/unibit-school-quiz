using System.ComponentModel.DataAnnotations;

namespace App.Controllers.Resources
{
    public class CredentialsResource
    {
        public string Username { get; set; }

        [Required]
        [StringLength(100, 
            ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
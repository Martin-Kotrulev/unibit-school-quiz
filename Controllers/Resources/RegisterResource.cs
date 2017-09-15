using System.ComponentModel.DataAnnotations;

namespace App.Controllers.Resources
{
  public class RegisterResource : CredentialsResource
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }
  }
}
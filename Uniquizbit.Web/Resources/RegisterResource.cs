using System.ComponentModel.DataAnnotations;

namespace Uniquizbit.Controllers.Resources
{
  public class RegisterResource : CredentialsResource
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }
  }
}
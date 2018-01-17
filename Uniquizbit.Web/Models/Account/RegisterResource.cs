namespace Uniquizbit.Web.Models
{
  using System.ComponentModel.DataAnnotations;
  
  public class RegisterResource : CredentialsResource
  {
    [Required]
    [EmailAddress]
    public string Email { get; set; }
  }
}
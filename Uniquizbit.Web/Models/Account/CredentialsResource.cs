namespace Uniquizbit.Web.Models
{
  using System.ComponentModel.DataAnnotations;

  public class CredentialsResource
  {
    [Required]
    public string Username { get; set; }

    [Required]
    [StringLength(100,
        ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 4)]
    [DataType(DataType.Password)]
    public string Password { get; set; }
  }
}
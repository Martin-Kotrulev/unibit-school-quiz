using System.ComponentModel.DataAnnotations;

namespace App.Models
{
  public class GroupSubscription
  {
    [Required]
    public string UserId { get; set; }

    [Required]
    public int QuizGroupId { get; set; }
  }
}
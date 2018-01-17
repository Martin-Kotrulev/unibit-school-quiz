namespace Uniquizbit.Data.Models
{
  using System.ComponentModel.DataAnnotations;
  public class GroupSubscription
  {
    [Required]
    public string UserId { get; set; }

    [Required]
    public int QuizGroupId { get; set; }
  }
}
namespace Uniquizbit.Data.Models
{
  using System.ComponentModel.DataAnnotations;
  
  public class QuizSubscription
  {
    [Required]
    public string UserId { get; set; }

    [Required]
    public int QuizId { get; set; }
  }
}
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
  public class QuizSubscription
  {
    [Required]
    public string UserId { get; set; }

    [Required]
    public int QuizId { get; set; }
  }
}
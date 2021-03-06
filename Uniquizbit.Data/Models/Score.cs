namespace Uniquizbit.Data.Models
{
  using System;
  using System.ComponentModel.DataAnnotations;

  public class Score
  {
    [Required]
    [Range(2.0, 6.0)]
    public double Value { get; set; }

    [Required]
    public DateTime ScoredAt { get; set; }

    public int QuizId { get; set; }

    public Quiz Quiz { get; set; }

    public string UserId { get; set; }

    public User User { get; set; }
  }
}
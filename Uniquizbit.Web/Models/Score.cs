using System;
using System.ComponentModel.DataAnnotations;

namespace Uniquizbit.Models
{
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

    public ApplicationUser User { get; set; }
  }
}
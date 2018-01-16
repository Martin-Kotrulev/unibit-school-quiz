using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Uniquizbit.Models
{
  public class QuizProgress
  {
    [Required]
    public string UserId { get; set; }

    [Required]
    public int QuizId { get; set; }

    [Required]
    public int QuestionId { get; set; }

    public ICollection<ProgressesAnswers> GivenAnswers { get; set; }

    [Required]
    public DateTime ValidTo { get; set; }

    public QuizProgress()
    {
        this.GivenAnswers = new HashSet<ProgressesAnswers>();
    }
  }
}
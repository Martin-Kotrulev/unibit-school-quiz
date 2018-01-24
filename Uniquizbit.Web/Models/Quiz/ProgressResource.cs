namespace Uniquizbit.Web.Models
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;

  public class ProgressResource
  {
    public string UserId { get; set; }

    [Required]
    public int QuizId { get; set; }

    public AnswerResource Answer { get; set; }
  }
}
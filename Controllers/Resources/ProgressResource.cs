using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Controllers.Resources
{
  public class ProgressResource
  {
    public string UserId { get; set; }

    [Required]
    public int QuizId { get; set; }

    [Required]
    public int QuestionId { get; set; }

    public ICollection<int> GivenAnswers { get; set; }

    [Required]
    public DateTime ValidTo { get; set; }

    public ProgressResource()
    {
      this.GivenAnswers = new HashSet<int>();
    }
  }
}
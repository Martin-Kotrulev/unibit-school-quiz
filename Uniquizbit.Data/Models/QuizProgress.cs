namespace Uniquizbit.Data.Models
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;

  public class QuizProgress
  {
    [Required]
    public string UserId { get; set; }

    [Required]
    public int QuizId { get; set; }

    public string QuestionsOrder { get; set; }

    public string AnswersOrder { get; set; }

    public ICollection<ProgressAnswer> GivenAnswers { get; set; }

    [Required]
    public DateTime ValidTo { get; set; } = DateTime.UtcNow.AddDays(3);

    public QuizProgress()
    {
      this.GivenAnswers = new HashSet<ProgressAnswer>();
    }
  }
}
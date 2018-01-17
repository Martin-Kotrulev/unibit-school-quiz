namespace Uniquizbit.Data.Models
{
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;

  public class Question
  {
    public int Id { get; set; }

    [Required]
    public string Value { get; set; }

    public int MaxAnswers { get; set; } = 5;

    public bool IsMultiselect { get; set; } = false;

    public int QuizId { get; set; }
    
    public Quiz Quiz { get; set; }

    public ICollection<Answer> Answers { get; set; }
  }
}
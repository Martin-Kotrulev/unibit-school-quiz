using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
  public class Question
  {
    public int Id { get; set; }

    [Required]
    public string Value { get; set; }

    public int MaxAnswers { get; set; } = 3;

    public bool IsMultiselect { get; set; }

    public int QuizId { get; set; }
    
    public Quiz Quiz { get; set; }

    public ICollection<Answer> Answers { get; set; }
  }
}
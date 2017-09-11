using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models
{
  public class Answer
  {
    public int Id { get; set; }

    [Required]
    public char Letter { get; set; }

    [Required]
    public string Value { get; set; }

    [Range(1, 10)]
    public int Weight { get; set; }

    public bool IsRight { get; set; } = false;

    [NotMapped]
    public bool IsSelected { get; set; } = false;

    public int QuestionId { get; set; }
    
    public Question Question { get; set; }

    public int QuizId { get; set; }

    public Quiz Quiz { get; set; }
  }
}
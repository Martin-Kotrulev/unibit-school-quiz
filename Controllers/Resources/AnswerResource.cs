using System.ComponentModel.DataAnnotations;

namespace App.Controllers.Resources
{
  public class AnswerResource
  {
    public int Id { get; set; }

    [Required]
    public char Letter { get; set; }

    [Required]
    public string Value { get; set; }

    [Range(1, 10)]
    public int Weight { get; set; } = 1;

    public bool IsRight { get; set; } = false;
    
    public bool IsSelected { get; set; } = false;

    public int QuestionId { get; set; }

    public int QuizId { get; set; }
  }
}
namespace Uniquizbit.Web.Models
{
  using System.ComponentModel.DataAnnotations;

  public class AnswerResource
  {
    public int Id { get; set; }

    public char Letter { get; set; }

    [Required]
    public string Value { get; set; }

    [Range(1, 10)]
    public int Weight { get; set; } = 1;

    public bool IsRight { get; set; } = false;

    public bool IsChecked { get; set; } = false;

    public int QuestionId { get; set; }

    public int QuizId { get; set; }
  }
}
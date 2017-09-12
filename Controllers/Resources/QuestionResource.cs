using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Controllers.Resources
{
  public class QuestionResource
  {
    public int Id { get; set; }

    [Required]
    public string Value { get; set; }

    public int MaxAnswers { get; set; } = 5;

    public bool IsMultiselect { get; set; } = false;

    public int QuizId { get; set; }

    public ICollection<AnswerResource> Answers { get; set; }

    public QuestionResource()
    {
      this.Answers = new HashSet<AnswerResource>();
    }
  }
}
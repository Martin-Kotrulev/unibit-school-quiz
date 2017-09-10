using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Controllers.Resources
{
  public class QuestionResource
  {
    public int Id { get; set; }

    [Required]
    public string Value { get; set; }

    public int MaxAnswers { get; set; } = 3;

    public bool IsMultiselect { get; set; }

    public int QuizId { get; set; }

    public ICollection<int> Answers { get; set; }

    public QuestionResource()
    {
      this.Answers = new HashSet<int>();
    }
  }
}
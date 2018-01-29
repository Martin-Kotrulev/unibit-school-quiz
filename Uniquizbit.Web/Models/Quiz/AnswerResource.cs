namespace Uniquizbit.Web.Models
{
  using System.ComponentModel.DataAnnotations;
  using Newtonsoft.Json;

  public class AnswerResource
  {
    public int Id { get; set; }

    [Required]
    public string Letter { get; set; }

    [Required]
    public string Value { get; set; }

    [Range(1, 10)]
    public int Weight { get; set; } = 1;

    public bool IsChecked { get; set; } = false;

    public bool IsRight { get; set; } = false;

    public int QuestionId { get; set; }

    public int QuizId { get; set; }

    [JsonIgnore]
    public bool IsOwnAnswer { get; set; } = false;

    public bool ShouldSerializeIsRight()
    {
      return IsOwnAnswer;
    }
  }
}
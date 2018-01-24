namespace Uniquizbit.Data.Models
{
  public class ProgressesAnswers
  {
    public int ProgressId { get; set; }

    public QuizProgress Progress { get; set; }

    public int ProgressAnswerId { get; set; }

    public ProgressAnswer ProgressAnswer { get; set; }
  }
}
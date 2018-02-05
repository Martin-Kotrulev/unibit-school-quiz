namespace Uniquizbit.Data.Models
{
  public class ProgressAnswer
  {
    public int Id { get; set; }

    public int AnswerId { get; set; }

    public Answer Answer { get; set; }

    public int QuestionId { get; set; }

    public QuizProgress QuizProgress { get; set; }

    public int QuizProgressId { get; set; }

    public bool IsChecked { get; set; }
  }
}
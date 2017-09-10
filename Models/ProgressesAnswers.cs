namespace App.Models
{
  public class ProgressesAnswers
  {
    public int ProgressId { get; set; }

    public QuizProgress Progress { get; set; }

    public int AnswerId { get; set; }
    
    public Answer Answer { get; set; }
  }
}
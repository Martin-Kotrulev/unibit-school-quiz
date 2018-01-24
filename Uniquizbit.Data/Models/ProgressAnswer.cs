namespace Uniquizbit.Data.Models
{
  public class ProgressAnswer
  {
    public int Id { get; set; }

    public int AnswerId { get; set; }

    public Answer Answer { get; set; }

		public int QuizId { get; set; }

    public bool IsChecked { get; set; }
  }
}
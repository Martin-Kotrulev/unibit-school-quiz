namespace Uniquizbit.Web.Models
{
    public class ProgressAnswerResource
    {
        public int QuizId { get; set; }

        public int AnswerId { get; set; }

        public bool IsChecked { get; set; }
    }
}
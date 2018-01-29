namespace Uniquizbit.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class ProgressAnswerResource
    {
        [Required]
        public int QuizId { get; set; }

        [Required]
        public int AnswerId { get; set; }

        [Required]
        public bool IsChecked { get; set; }
    }
}
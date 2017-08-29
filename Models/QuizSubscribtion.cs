using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class QuizSubscribtion
    {
        [Required]
        public string UserId { get; set; }

        [Required]
        public int QuizId { get; set; }
    }
}
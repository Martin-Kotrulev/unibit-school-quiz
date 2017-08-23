using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Core.Models
{
    public class Rating
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public double Value { get; set; }

        public ApplicationUser User { get; set; }

        [ForeignKey("Quiz")]
        public int QuizId { get; set; }

        public Quiz Quiz { get; set; }
    }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace App.Core.Models
{
    public class QuizProgress
    {
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public int QuizId { get; set; }

        [Required]
        public int QuestionId { get; set; }

        [Required]
        public int AnswerId { get; set; }

        [Required]
        public DateTime ValidTo { get; set; }
    }
}
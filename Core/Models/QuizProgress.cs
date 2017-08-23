using System;

namespace App.Core.Models
{
    public class QuizProgress
    {
        public int Id { get; set; }

        public int QuizId { get; set; }

        public int AnswerId { get; set; }

        public DateTime ValidTo { get; set; }
    }
}
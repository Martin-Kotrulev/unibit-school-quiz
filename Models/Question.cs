using System.Collections.Generic;

namespace App.Models
{
    public class Question
    {
        public int Id { get; set; }

        public string Value { get; set; }

        public int MaxAnswers { get; set; } = 3;

        public ICollection<Answer> Answers { get; set; }

        public ICollection<Answer> RightAnswers { get; set; }
    }
}
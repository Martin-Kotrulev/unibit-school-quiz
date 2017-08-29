using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Required]
        public string Value { get; set; }

        public int MaxAnswers { get; set; } = 3;

        public ICollection<Answer> Answers { get; set; }

        public ICollection<Answer> RightAnswers { get; set; }
    }
}
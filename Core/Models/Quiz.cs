using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Core.Models
{
    public class Quiz
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public int MaxAnswers { get; set; } = 3;

        public DateTime StartDateTime { get; set; }

        public DateTime EndDateTime { get; set; }

        public bool IsOneTime { get; set; }

        public int QuizGroupId { get; set; }

        public QuizGroup QuizGroup { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public ICollection<Answer> Answers { get; set; }

        public ICollection<Score> Scores { get; set; }

        public Quiz()
        {
            Tags = new HashSet<Tag>();
            Answers = new HashSet<Answer>();
            Scores = new HashSet<Score>();
        }
    }
}
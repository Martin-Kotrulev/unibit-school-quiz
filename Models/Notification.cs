using System;

namespace App.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime Date { get; set; }

        public Quiz Quiz { get; set; }

        public QuizGroup QuizGroup { get; set; }

        public ApplicationUser Issuer { get; set; }
    }
}
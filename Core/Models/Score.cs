namespace App.Core.Models
{
    public class Score
    {
        public int Id { get; set; }

        public double Value { get; set; }

        public int QuizId { get; set; }

        public Quiz Quiz { get; set; }

        public ApplicationUser User { get; set; }
    }
}
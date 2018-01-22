namespace Uniquizbit.Data.Models
{
    public class QuizzesUsers
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int QuizId { get; set; }

        public Quiz Quiz { get; set; }

        public bool Finished { get; set; }
    }
}
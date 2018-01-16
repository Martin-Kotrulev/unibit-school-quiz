namespace Uniquizbit.Models
{
    public class QuizzesUsers
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public int QuizId { get; set; }

        public Quiz Quiz { get; set; }
    }
}
namespace App.Core.Models
{
    public class UsersGroups
    {
        public string UserId { get; set; }

        public int QuizGroupId { get; set; }

        public ApplicationUser User { get; set; }

        public QuizGroup QuizGroup { get; set; }
    }
}
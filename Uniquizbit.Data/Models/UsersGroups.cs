namespace Uniquizbit.Data.Models
{
  public class UsersGroups
  {
    public string UserId { get; set; }

    public User User { get; set; }

    public int QuizGroupId { get; set; }

    public QuizGroup QuizGroup { get; set; }
  }
}
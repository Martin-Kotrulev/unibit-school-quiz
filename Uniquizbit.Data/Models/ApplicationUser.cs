namespace Uniquizbit.Data.Models
{
  using System.Collections.Generic;
  using Microsoft.AspNetCore.Identity;
  using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

  public class User : IdentityUser
  {
    public ICollection<Quiz> OwnQuizzes { get; set; }

    public ICollection<QuizzesUsers> TakenQuizzes { get; set; }

    public ICollection<Score> Scores { get; set; }

    public ICollection<QuizGroup> OwnGroups { get; set; }

    public ICollection<UsersGroups> InQuizGroups { get; set; }

    public ICollection<Notification> Notifications { get; set; }

    public User()
    {
      OwnQuizzes = new HashSet<Quiz>();
      TakenQuizzes = new HashSet<QuizzesUsers>();
      Scores = new HashSet<Score>();
      OwnGroups = new HashSet<QuizGroup>();
      InQuizGroups = new HashSet<UsersGroups>();
      Notifications = new HashSet<Notification>();
    }
  }
}
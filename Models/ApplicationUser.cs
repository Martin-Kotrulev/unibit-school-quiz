using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace App.Models
{
  public class ApplicationUser : IdentityUser
  {
    public ICollection<Quiz> OwnQuizes { get; set; }

    public ICollection<Quiz> TakenQuizes { get; set; }

    public ICollection<Score> Scores { get; set; }

    public ICollection<QuizGroup> OwnGroups { get; set; }

    public ICollection<QuizGroup> InQuizGroups { get; set; }

    public ICollection<Notification> Notifications { get; set; }
    
    public ApplicationUser() {
      OwnQuizes = new HashSet<Quiz>();
      TakenQuizes = new HashSet<Quiz>();
      Scores = new HashSet<Score>();
      OwnGroups = new HashSet<QuizGroup>();
      InQuizGroups = new HashSet<QuizGroup>();
      Notifications = new HashSet<Notification>();
    }
  }
}
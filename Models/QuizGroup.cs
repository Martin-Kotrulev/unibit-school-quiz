using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
  public class QuizGroup
  {
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; }

    public ApplicationUser Owner { get; set; }

    public ICollection<Quiz> Quizes { get; set; }

    public ICollection<UsersGroups> Members { get; set; }

    public ICollection<Tag> Tags { get; set; }

    public QuizGroup()
    {
      Quizes = new HashSet<Quiz>();
      Members = new HashSet<UsersGroups>();
      Tags = new HashSet<Tag>();
    }
  }
}
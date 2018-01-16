using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Uniquizbit.Models
{
  public class QuizGroup
  {
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; }

    public DateTime CreatedOn { get; set; }

    public string CreatorId { get; set; }

    public string CreatorName { get; set; }
    
    public ApplicationUser Creator { get; set; }

    public ICollection<Quiz> Quizzes { get; set; }

    public ICollection<UsersGroups> Members { get; set; }

    public ICollection<GroupsTags> Tags { get; set; }

    public QuizGroup()
    {
      Quizzes = new HashSet<Quiz>();
      Members = new HashSet<UsersGroups>();
      Tags = new HashSet<GroupsTags>();
    }
  }
}
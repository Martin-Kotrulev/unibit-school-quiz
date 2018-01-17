namespace Uniquizbit.Data.Models
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;

  public class Quiz
  {
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? Starts { get; set; }

    public DateTime? Ends { get; set; }

    public bool Once { get; set; } = false;

    public string Password { get; set; }

    public bool Locked { get; set; } = false;

    public int? GroupId { get; set; }

    public QuizGroup Group { get; set; }

    public string CreatorId { get; set; }

    public string CreatorName { get; set; }
    
    public ApplicationUser Creator { get; set; }

    public ICollection<QuizzesTags> Tags { get; set; }

    public ICollection<Question> Questions { get; set; }

    public ICollection<Score> Scores { get; set; }

    public ICollection<QuizzesUsers> Participants { get; set; }

    public Quiz()
    {
      Tags = new HashSet<QuizzesTags>();
      Questions = new HashSet<Question>();
      Scores = new HashSet<Score>();
      Participants = new HashSet<QuizzesUsers>();
    }
  }
}
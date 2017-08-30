using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Models
{
  public class Quiz
  {
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Title { get; set; }

    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public bool IsOneTime { get; set; } = false;

    public string Password { get; set; }

    public bool Locked { get; set; } = false;

    public QuizGroup QuizGroup { get; set; }

    public ICollection<Tag> Tags { get; set; }

    public ICollection<Question> Questions { get; set; }

    public ICollection<Score> Scores { get; set; }

    public Quiz()
    {
      Tags = new HashSet<Tag>();
      Questions = new HashSet<Question>();
      Scores = new HashSet<Score>();
    }
  }
}
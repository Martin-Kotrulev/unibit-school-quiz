using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Models
{
  public class Answer
  {
    public int Id { get; set; }

    [Required]
    public string Value { get; set; }

    public bool IsRight { get; set; } = false;

    [NotMapped]
    public bool IsSelected { get; set; } = false;

    public int QuestionId { get; set; }
    
    public Question Question { get; set; }
  }
}
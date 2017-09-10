using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace App.Controllers.Resources
{
  public class QuizResource
  {
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Title { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime StartDateTime { get; set; }

    public DateTime EndDateTime { get; set; }

    public bool IsOneTime { get; set; } = false;

    public bool Locked { get; set; } = false;

    public string Password { get; set; }

    public string CreatorId { get; set; }

    public int? QuizGroupId { get; set; }

    public ICollection<string> Tags { get; set; }

    public int Scores { get; set; }

    public int Participants { get; set; }

    public QuizResource()
    {
      Tags = new HashSet<string>();
    }
  }
}
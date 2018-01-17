namespace Uniquizbit.Web.Models
{
  using Newtonsoft.Json;
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations;

  public class QuizResource
  {
    public int Id { get; set; }

    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? Starts { get; set; }

    public DateTime? Ends { get; set; }

    public bool Once { get; set; } = false;

    public bool Locked { get; set; } = false;

    public string Password { get; set; }

    public string CreatorId { get; set; }

    public string CreatorName { get; set; }

    public int? GroupId { get; set; }

    public ICollection<string> Tags { get; set; }

    public int Scores { get; set; }

    public int Participants { get; set; }

    public QuizResource()
    {
      Tags = new HashSet<string>();
    }
  }
}
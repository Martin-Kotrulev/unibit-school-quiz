using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace App.Controllers.Resources
{
  public class QuizGroupResource
  {
    public int Id { get; set; }

    [Required]
    [StringLength(255)]
    public string Name { get; set; }

    public DateTime CreatedOn { get; set; }

    public string OwnerId { get; set; }

    public ICollection<string> Tags { get; set; }

    public QuizGroupResource()
    {
      Tags = new HashSet<string>();
    }
  }
}
using System;
using System.ComponentModel.DataAnnotations;

namespace Uniquizbit.Models
{
  public class Notification
  {
    public int Id { get; set; }

    public string Message { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public Quiz Quiz { get; set; }

    public QuizGroup QuizGroup { get; set; }

    public ApplicationUser Issuer { get; set; }

    public bool Seen { get; set; } = false;
  }
}
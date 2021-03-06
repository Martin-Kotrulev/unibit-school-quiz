namespace Uniquizbit.Data.Models
{
  using System;
  using System.ComponentModel.DataAnnotations;

  public class Notification
  {
    public int Id { get; set; }

    public string Message { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public Quiz Quiz { get; set; }

    public QuizGroup QuizGroup { get; set; }

    public User Issuer { get; set; }

    public bool Seen { get; set; } = false;
  }
}
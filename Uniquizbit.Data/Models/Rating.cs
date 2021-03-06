namespace Uniquizbit.Data.Models
{
  using System.ComponentModel.DataAnnotations;
  using System.ComponentModel.DataAnnotations.Schema;

  public class Rating
  {
    [Range(1.0, 5.0)]
    public double Value { get; set; }

    public string UserId { get; set; }

    public User User { get; set; }

    public int QuizId { get; set; }

    public Quiz Quiz { get; set; }
  }
}
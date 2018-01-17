namespace Uniquizbit.Data.Models
{
  using System.ComponentModel.DataAnnotations;

  public class Tag
  {
    public int Id { get; set; }

    [Required]
    [MaxLength(20)]
    public string Name { get; set; }
  }
}
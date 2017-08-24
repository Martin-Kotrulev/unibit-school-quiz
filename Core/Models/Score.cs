using System.ComponentModel.DataAnnotations;

namespace App.Core.Models
{
    public class Score
    {
        public int Id { get; set; }

        [Required]
        [Range(2.0, 6.0)]
        public double Value { get; set; }

        public Quiz Quiz { get; set; }

        public ApplicationUser User { get; set; }
    }
}